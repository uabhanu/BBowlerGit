using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour 
{
    Animator m_animator;
    BhanuAction m_bhanuAction = new BhanuAction(); //Keep this here in global scope as we need only 1 instance
    Ball m_ball;
    float m_lastChangeTime , m_pinSetterBallResetTime = 5f;
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

    IEnumerator BallResetRoutine()
    {
        yield return new WaitForSeconds(m_ballResetTime);
        m_ball.Reset();   
        m_ballResetTime = m_pinSetterBallResetTime;
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

	void PinsStandingAndSettle()
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
        m_lastSettledCount = standingPins; //Commented this line if any issues

        BhanuAction.Action action = m_bhanuAction.Bowl(pinsFell);
        Debug.Log("Pins Fell : " + pinsFell + " " + action);

        if(action == BhanuAction.Action.TIDY)
        {
            if(StandingPins() < 10)
            {
                m_animator.SetTrigger("Tidy");
                m_ballResetTime = m_pinSetterBallResetTime;    
            }

            StartCoroutine("BallResetRoutine");   
        }

        else if(action == BhanuAction.Action.ENDTURN || action == BhanuAction.Action.RESET)
        {
            m_animator.SetTrigger("Reset");
            m_ballResetTime = m_pinSetterBallResetTime;
            m_lastSettledCount = 10;
            StartCoroutine("BallResetRoutine");
        }

        else if(action == BhanuAction.Action.ENDGAME)
        {
            throw new UnityException("Sir Bhanu, You haven't told me how to handle EndGame yet");
        }
    }

    public void PinsLower()
    {
        Debug.Log("Pins Lower");
        m_standingPinsDisplayOutlineColour = Color.green;
        m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;
        m_standingPinsTextOutlineColour = Color.green;
        m_standingPinsTextOutline.effectColor = m_standingPinsTextOutlineColour;

        foreach(Pin pin in m_pins)
        {
            pin.Lower();
        }
    }

    public void PinsRaise()
    {
        Debug.Log("Pins Setter Pins Raise");
        m_standingPinsDisplayOutlineColour = Color.green;
        m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;
        m_standingPinsTextOutlineColour = Color.green;
        m_standingPinsTextOutline.effectColor = m_standingPinsTextOutlineColour;

        foreach(Pin pin in m_pins)
        {
            pin.RaiseIfStanding();
        }
    }

    public void PinsRenew()
    {
        Debug.Log("Pins Renew");
        m_standingPinsDisplayOutlineColour = Color.green;
        m_standingPinsDisplayOutline.effectColor = m_standingPinsDisplayOutlineColour;
        m_standingPinsTextOutlineColour = Color.green;
        m_standingPinsTextOutline.effectColor = m_standingPinsTextOutlineColour;
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
