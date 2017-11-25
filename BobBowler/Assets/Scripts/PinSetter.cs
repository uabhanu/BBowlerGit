using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour 
{
    Ball m_ball;
    bool m_ballEnteredBox;
	[SerializeField] Color m_standingPinsDisplayOutlineColour;
    float m_lastChangeTime;
	int m_lastStandingCount = -1;

	[SerializeField] Outline m_standingPinsDisplayOutline;
	[SerializeField] Text m_standingPinsDisplay;

	void Start() 
	{
        m_ball = FindObjectOfType<Ball>();
		m_standingPinsDisplayOutlineColour = m_standingPinsDisplayOutline.effectColor;
        m_standingPinsDisplayOutlineColour = Color.blue;
        m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;
	}

	void Update() 
	{
		if(Time.timeScale == 0)
		{
			return;
		}
			
		m_standingPinsDisplay.text = StandingPins().ToString();

        if(m_ballEnteredBox)
        {
            CheckStanding();
        }
	}

	void CheckStanding()
	{
		int currentStanding = StandingPins();

        if(currentStanding != m_lastStandingCount)
        {
            m_lastChangeTime = Time.time;
            m_lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f;

        if((Time.time  - m_lastChangeTime) > settleTime)
        {
            PinsHaveSettled();
        }
	}

	void OnTriggerEnter(Collider tri)
	{
		if(tri.gameObject.name.Equals("PF_Ball"))
		{
			m_ballEnteredBox = true;
			m_standingPinsDisplayOutlineColour = Color.red;
			m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;
		}
	}

    void PinsHaveSettled()
    {
        m_ballEnteredBox = false;
        m_lastStandingCount = -1;
        m_standingPinsDisplayOutlineColour = Color.green;
        m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;
        m_ball.Reset();
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
