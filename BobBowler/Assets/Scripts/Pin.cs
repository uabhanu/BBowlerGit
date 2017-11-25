using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour 
{
	[SerializeField] float m_standingThreshold;

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
		Vector3 rotationInEuler = transform.rotation.eulerAngles;

		float tiltX = Mathf.Abs(rotationInEuler.x);
		float tiltZ = Mathf.Abs(rotationInEuler.z);

		return(tiltX < m_standingThreshold || tiltZ < m_standingThreshold);
	}
}
