﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	AudioSource m_ballSound;
    bool m_inPlay = false;
    PinSetter m_pinSetter;
    Quaternion m_startRotation;
	Rigidbody m_ballBody;
    Vector3 m_startPosition;

	void Start() 
	{
		m_ballBody = GetComponent<Rigidbody>();	
		m_ballBody.useGravity = false;
        m_pinSetter = FindObjectOfType<PinSetter>();
        m_startPosition = transform.position;
        m_startRotation = transform.rotation;
	}
		
	public void Launch(Vector3 velocity)
	{
        if(!m_inPlay)
        {
            m_ballBody.useGravity = true;
            m_ballBody.velocity = velocity;
            m_ballSound = GetComponent<AudioSource>();
            m_ballSound.Play();   
            m_inPlay = true;
        }
	}

    public void Nudge(float amount)
    {
        if(!m_inPlay)
        {
            transform.Translate(new Vector3(amount , 0f , 0f));      
        }
    }

    public void Reset()
    {
        //Debug.Log("Ball Reset");
        m_ballBody.angularVelocity = Vector3.zero;
        m_ballBody.useGravity = false;
        m_ballBody.velocity = Vector3.zero;
        m_inPlay = false;
        transform.position = m_startPosition;
        transform.rotation = m_startRotation;
        m_pinSetter.m_standingPinsDisplayOutlineColour = Color.green;
        m_pinSetter.m_standingPinsDisplayOutline.effectColor = m_pinSetter.m_standingPinsDisplayOutlineColour;
        m_pinSetter.m_standingPinsTextOutlineColour = Color.green;
        m_pinSetter.m_standingPinsTextOutline.effectColor = m_pinSetter.m_standingPinsTextOutlineColour;
    }
}
