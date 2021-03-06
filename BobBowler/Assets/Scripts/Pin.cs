﻿using System.Collections;
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
            m_pinBody.useGravity = false;
            transform.Translate(new Vector3(0f , m_distanceToRaise , 0f) , Space.World); 
            transform.rotation = Quaternion.Euler(270f , 0f , 0f);
        }
        else
        {
            Debug.LogError("Sir Bhanu, there are no pins, let alone standing");
        }
    }
}
