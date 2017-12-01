using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour 
{
    Animator m_animator;
    BhanuAction m_bhanuAction = new BhanuAction(); //Keep this here in global scope as we need only 1 instance
    Ball m_ball;
    float m_lastChangeTime , m_pinSetterBallResetTime = 15f;
    GameObject m_pinsPrefabInScene;
    GutterBallCheck m_gutterBallCheck;
	int m_lastSettledCount = 10 , m_lastStandingCount = -1;
    Pin[] m_pins;

    [SerializeField] GameObject m_pinsPrefab;

    public bool m_ballOutOfPlay;
    public Color m_standingPinsDisplayOutlineColour , m_standingPinsTextOutlineColour;
    public float m_ballResetTime;
    public Outline m_standingPinsDisplayOutline , m_standingPinsTextOutline;
    public Text m_standingPinsDisplay;

	void Start() 
	{
        m_animator = GetComponent<Animator>();
        m_ball = FindObjectOfType<Ball>();
        m_ballResetTime = m_pinSetterBallResetTime;
        m_gutterBallCheck = FindObjectOfType<GutterBallCheck>();
        m_pins = FindObjectsOfType<Pin>();

		m_standingPinsDisplayOutlineColour = m_standingPinsDisplayOutline.effectColor;
        m_standingPinsTextOutlineColour = m_standingPinsTextOutline.effectColor;
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
            m_ball.Reset();
        }
	}
    
	void OnTriggerEnter(Collider tri)
	{
		if(tri.gameObject.tag.Equals("Ball"))
		{
            m_ballOutOfPlay = true;
			m_standingPinsDisplayOutlineColour = Color.red;
			m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;
            m_standingPinsTextOutlineColour = Color.red;
            m_standingPinsTextOutline.effectColor = m_standingPinsTextOutlineColour;
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

        BhanuAction.Action action = m_bhanuAction.Bowl(pinsFell);
        //Debug.Log("Pins Fell : " + pinsFell + " " + action);
        //Debug.Log("Pins Standing : " + standingPins + " " + action);

        if(action == BhanuAction.Action.TIDY)
        {
            m_animator.SetTrigger("Tidy");
            m_ballResetTime = m_pinSetterBallResetTime;    
        }

        else if(action == BhanuAction.Action.ENDTURN || action == BhanuAction.Action.RESET)
        {
            m_animator.SetTrigger("Reset");
            m_ballResetTime = m_pinSetterBallResetTime;
            m_lastSettledCount = 10;
        }

        else if(action == BhanuAction.Action.ENDGAME)
        {
            throw new UnityException("Sir Bhanu, You haven't told me how to handle EndGame yet");
        }
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
            pin.transform.rotation = Quaternion.Euler(270f , 0f , 0f);
        }
    }

    public void PinsRenew()
    {
        Instantiate(m_pinsPrefab , new Vector3(0f , 0f , 1829f) , Quaternion.identity);
        m_pinsPrefabInScene = GameObject.FindGameObjectWithTag("Pins");

        if(m_pinsPrefabInScene.transform.childCount == 0)
        {
            Destroy(m_pinsPrefabInScene.gameObject);
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
