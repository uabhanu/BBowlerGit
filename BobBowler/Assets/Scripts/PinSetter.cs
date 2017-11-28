using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour 
{
    Ball m_ball;
    bool m_ballEnteredBox;
	Color m_standingPinsDisplayOutlineColour , m_standingPinsTextOutlineColour;
    float m_lastChangeTime;
	int m_lastStandingCount = -1;
    Pin[] m_pins;

    [SerializeField] Outline m_standingPinsDisplayOutline , m_standingPinsTextOutline;
    [SerializeField] Text m_standingPinsDisplay;

	void Start() 
	{
        m_ball = FindObjectOfType<Ball>();
        m_pins = FindObjectsOfType<Pin>();

		m_standingPinsDisplayOutlineColour = m_standingPinsDisplayOutline.effectColor;
        m_standingPinsDisplayOutlineColour = Color.blue;
        m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;

        m_standingPinsTextOutlineColour = m_standingPinsTextOutline.effectColor;
        m_standingPinsTextOutlineColour = Color.blue;
        m_standingPinsTextOutline.effectColor = m_standingPinsTextOutlineColour;
	}

    IEnumerator BallResetRoutine()
    {
        yield return new WaitForSeconds(2f);
        m_ball.Reset();

        m_standingPinsDisplayOutlineColour = Color.blue;
        m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;

        m_standingPinsTextOutlineColour = Color.blue;
        m_standingPinsTextOutline.effectColor = m_standingPinsTextOutlineColour;
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
            m_standingPinsTextOutlineColour = Color.red;
            m_standingPinsTextOutline.effectColor = m_standingPinsTextOutlineColour;
		}
	}

    void PinsHaveSettled()
    {
        m_ballEnteredBox = false;
        m_lastStandingCount = -1;
        m_standingPinsDisplayOutlineColour = Color.green;
        m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;
        m_standingPinsTextOutlineColour = Color.green;
        m_standingPinsTextOutline.effectColor = m_standingPinsTextOutlineColour;
        StartCoroutine("BallResetRoutine");
    }

    public void PinsLower()
    {
        foreach(Pin pin in m_pins)
        {
            pin.Lower();
        }
    }

    public void PinsRaise()
    {
        foreach(Pin pin in m_pins)
        {
            pin.RaiseIfStanding();
        }
    }

    public void PinsRenew()
    {
        foreach(Pin pin in m_pins)
        {
            pin.Renew();
        }
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
