using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    Ball m_ball;
    List<int> m_bowls = new List<int>();
    PinSetter m_pinSetter;

	void Start() 
    {
		m_ball = FindObjectOfType<Ball>();
        m_pinSetter = FindObjectOfType<PinSetter>();
	}

	public void Bowl(int pinFall) 
    {
		m_bowls.Add(pinFall);
        BhanuAction.Action nextAction = BhanuAction.NextAction(m_bowls);
        m_pinSetter.PerformAction(nextAction);
        m_ball.Reset();
	}
}
