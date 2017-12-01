using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour 
{
    float m_lastChangeTime;
    GameManager m_gameManager;
    int m_lastSettledCount = 10 , m_lastStandingCount = -1;

    public bool m_ballOutOfPlay;
    public Color m_standingPinsDisplayOutlineColour , m_standingPinsTextOutlineColour;
    public Outline m_standingPinsDisplayOutline , m_standingPinsTextOutline;
    public Text m_standingPinsDisplay;

	void Start() 
    {
        m_gameManager = FindObjectOfType<GameManager>();
	}

	void Update() 
    {
        if(Time.timeScale == 0)
        {
            return;
        }
            
        m_standingPinsDisplay.text = StandingPins().ToString();

        if(m_ballOutOfPlay)
        {
            PinsStandingAndSettle();
        }
	}

    void OnTriggerExit(Collider tri)
    {
        if(tri.gameObject.tag.Equals("Ball"))
        {
            m_ballOutOfPlay = true;
            m_standingPinsDisplayOutlineColour = Color.red;
            m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;
            m_standingPinsTextOutlineColour = Color.red;
            m_standingPinsTextOutline.effectColor = m_standingPinsTextOutlineColour;
            PinsStandingAndSettle();
        }
    }

    void PinsHaveSettled()
    {
        m_standingPinsDisplayOutlineColour = Color.green;
        m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;
        m_standingPinsTextOutlineColour = Color.green;
        m_standingPinsTextOutline.effectColor = m_standingPinsTextOutlineColour;

        m_ballOutOfPlay = false;
        m_lastStandingCount = -1;
        int standingPins = StandingPins();
        int pinsFell = m_lastSettledCount - standingPins;
        m_lastSettledCount = standingPins; //Comment this line if any issues
        m_gameManager.Bowl(pinsFell);

        //Debug.Log("Pins Fell : " + pinsFell + " " + action);
        //Debug.Log("Pins Standing : " + standingPins + " " + action);
    }

    public void PinsStandingAndSettle()
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

    public void Reset()
    {
        m_lastSettledCount = 10;
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
