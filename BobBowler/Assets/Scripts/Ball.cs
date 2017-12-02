using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	AudioSource m_ballSound;
    bool m_inPlay = false;
    PinCounter m_pinCounter;
    Quaternion m_startRotation;
	Rigidbody m_ballBody;
    Vector3 m_startPosition;

	void Start() 
	{
		m_ballBody = GetComponent<Rigidbody>();	
		m_ballBody.useGravity = false;
        m_pinCounter = FindObjectOfType<PinCounter>();
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
        float xPos = Mathf.Clamp(transform.position.x + amount , -43.5f , 43.5f);
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        if(!m_inPlay)
        {
            transform.position = new Vector3(xPos , yPos , zPos);
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
        m_pinCounter.m_standingPinsDisplayOutlineColour = Color.green;
        m_pinCounter.m_standingPinsDisplayOutline.effectColor = m_pinCounter.m_standingPinsDisplayOutlineColour;
        m_pinCounter.m_standingPinsTextOutlineColour = Color.green;
        m_pinCounter.m_standingPinsTextOutline.effectColor = m_pinCounter.m_standingPinsTextOutlineColour;
    }
}
