using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	AudioSource m_ballSound;
	Rigidbody m_ballBody;

	void Start() 
	{
		m_ballBody = GetComponent<Rigidbody>();	
		m_ballBody.useGravity = false;
	}
		
	public void Launch(Vector3 velocity)
	{
		m_ballBody.useGravity = true;
		m_ballBody.velocity = velocity;
		m_ballSound = GetComponent<AudioSource>();
		m_ballSound.Play();
	}
}
