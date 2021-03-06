﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	Vector3 m_offset;

	[SerializeField] Ball m_ball;
    [SerializeField] float m_stopPosition;

	void Start()
	{
		m_offset = transform.position - m_ball.transform.position;	
	}

	void Update() 
	{
		if(Time.timeScale == 0)
		{
			return;
		}

        if(m_ball.transform.position.z <= m_stopPosition)
		{
			transform.position = m_ball.transform.position + m_offset;	
		}
	}
}
