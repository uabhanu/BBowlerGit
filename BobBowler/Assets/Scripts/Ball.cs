using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	AudioSource m_ballSound;
	Rigidbody m_ballBody;

	[SerializeField] Vector3 m_launchVelocity;

	void Start() 
	{
		m_ballBody = GetComponent<Rigidbody>();	
		m_ballSound = GetComponent<AudioSource>();

		Launch();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
		{
			return;
		}
	}

	public void Launch()
	{
		m_ballBody.velocity = m_launchVelocity;
		m_ballSound.Play ();
	}
}
