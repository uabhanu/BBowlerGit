using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour 
{
	[SerializeField] Text m_standingPinsDisplay;

	void Start() 
	{
		
	}

	void Update() 
	{
		if(Time.timeScale == 0)
		{
			return;
		}
			
		m_standingPinsDisplay.text = StandingPins().ToString();
	}

	int StandingPins()
	{
		int standingPins = 0;

		foreach(Pin pin in FindObjectsOfType<Pin>())
		{
			if(pin.IsStanding())
			{
				standingPins++;
			}
		}

		return standingPins;
	}
}
