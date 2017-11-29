using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour 
{
    Animator m_animator;
    BhanuAction m_bhanuAction = new BhanuAction();
    Ball m_ball;
    bool m_ballEnteredBox;
	Color m_standingPinsDisplayOutlineColour , m_standingPinsTextOutlineColour;
    float m_lastChangeTime;
    GameObject m_pinsPrefabInScene;
	int m_lastSettledCount = 10 , m_lastStandingCount = -1;
    Pin[] m_pins;

    [SerializeField] GameObject m_pinsPrefab;
    [SerializeField] Outline m_standingPinsDisplayOutline , m_standingPinsTextOutline;
    [SerializeField] Text m_standingPinsDisplay;

	void Start() 
	{
        m_animator = GetComponent<Animator>();
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
        yield return new WaitForSeconds(5f);
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

        int standingPins = StandingPins();
        int pinsFell = m_lastSettledCount - standingPins;
        m_lastSettledCount = standingPins; //Commented this line if any issues
        BhanuAction.Action action = m_bhanuAction.Bowl(pinsFell);

        if(action == BhanuAction.Action.TIDY)
        {
            m_animator.SetTrigger("Tidy");
            StartCoroutine("BallResetRoutine");
        }

        else if(action == BhanuAction.Action.ENDTURN || action == BhanuAction.Action.RESET)
        {
            m_animator.SetTrigger("Reset");
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
        Debug.Log("Pins Renew");
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
