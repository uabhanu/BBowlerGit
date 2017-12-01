using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour 
{
    Pin[] m_pins;
    Rigidbody m_pinBody;

    [SerializeField] bool m_isStanding;
    [SerializeField] float m_distanceToRaise , m_standingThreshold;

    void Start()
    {
        m_pinBody = GetComponent<Rigidbody>();
        m_pins = FindObjectsOfType<Pin>();
    }

	void Update() 
	{
		if(Time.timeScale == 0)
		{
			return;
		}

		IsStanding();

        if(IsStanding())
        {
            m_isStanding = true;
        }

        if(!IsStanding())
        {
            m_isStanding = false;
        }
	}

	public bool IsStanding()
	{
        if(m_pinBody != null)
        {
            Vector3 rotationInEuler = transform.rotation.eulerAngles;

            float tiltX = Mathf.Abs(rotationInEuler.x);
            float tiltZ = Mathf.Abs(rotationInEuler.z);

            //Debug.Log(tiltX < m_standingThreshold || tiltZ < m_standingThreshold);
            return(tiltX < m_standingThreshold || tiltZ < m_standingThreshold);
        }
        else
        {
            Debug.LogError("Sir Bhanu, there are no pins, let alone standing");
        }

        return true;
	}

    public void Lower()
    {
        if(m_pinBody != null)
        {
            transform.Translate(new Vector3(0f , -m_distanceToRaise , 0f) , Space.World);
            m_pinBody.useGravity = true;
        }
        else
        {
            Debug.LogError("Sir Bhanu, there are no pins to Lower");
        }
    }

	void OnTriggerExit(Collider tri)
	{
		if(tri.gameObject.name.Equals("PinSetter"))
		{
			Destroy(gameObject);
		}
	}

    public void RaiseIfStanding()
    {
        if(m_pinBody != null && m_isStanding) 
        {
            //Debug.Log("Pins Raise");
            Debug.Log(m_pinBody.gameObject.name);
            m_pinBody.useGravity = false;
            transform.Translate(new Vector3(0f , m_distanceToRaise , 0f) , Space.World); 
            //This is only applying randomly to selected number of standing pins rather than all standing pins, question posted on Udemy
        }
        else
        {
            Debug.LogError("Sir Bhanu, there are no pins, let alone standing");
        }
    }
}
