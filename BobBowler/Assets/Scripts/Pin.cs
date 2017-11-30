using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour 
{
    Rigidbody m_pinBody;

    [SerializeField] float m_distanceToRaise , m_standingThreshold;

    void Start()
    {
        m_pinBody = GetComponent<Rigidbody>();
    }

	void Update() 
	{
		if(Time.timeScale == 0)
		{
			return;
		}

		IsStanding();
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
            transform.Translate(new Vector3(0f , -m_distanceToRaise , 0f));
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
        if(IsStanding()) 
        {
            Debug.Log("Pins Raise");
            m_pinBody.useGravity = false;
            transform.Translate(new Vector3(0f , m_distanceToRaise , 0f)); //This is moving pin forward instead of up, easy debug
        }
    }
}
