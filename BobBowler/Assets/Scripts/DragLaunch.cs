using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour 
{
	Ball m_ball;
	bool m_ballLaunched = false;
	float m_startTime , m_endTime;
	Vector3 m_dragStart , m_dragEnd;

	void Start() 
	{
		m_ball = GetComponent<Ball>();
	}

	public void DragStart()
	{
		m_dragStart = Input.mousePosition;
		m_startTime = Time.time;
	}

	public void DragEnd()
	{
		m_dragEnd = Input.mousePosition;
		m_endTime = Time.time;

		float dragDuration = m_endTime - m_startTime;

		float launchVelocityX = (m_dragEnd.x - m_dragStart.x) / dragDuration;
		float launchVelocityZ = (m_dragEnd.y - m_dragStart.y) / dragDuration;

		Vector3 launchVelocity = new Vector3(launchVelocityX , 0f , launchVelocityZ);

		if(!m_ballLaunched)
		{
			m_ball.Launch(launchVelocity);
			m_ballLaunched = true;
		}
	}

	public void NudgeBall(float amount)
	{
		if(!m_ballLaunched)
		{
			m_ball.transform.Translate(new Vector3(amount , 0f , 0f));		
		}
	}
}
