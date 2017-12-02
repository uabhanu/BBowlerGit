using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    Ball m_ball;
    List<int> m_rolls = new List<int>();
    PinSetter m_pinSetter;
    ScoreDisplay m_scoreDisplay;

	void Start() 
    {
		m_ball = FindObjectOfType<Ball>();
        m_pinSetter = FindObjectOfType<PinSetter>();
        m_scoreDisplay = FindObjectOfType<ScoreDisplay>();
	}

	public void Bowl(int pinFall) 
    {
        try
        {
            m_rolls.Add(pinFall);
            BhanuAction.Action nextAction = BhanuAction.NextAction(m_rolls);
            m_pinSetter.PerformAction(nextAction);
            m_ball.Reset();
        }
        catch
        {
            Debug.LogWarning("Sir Bhanu, Something seems to be out of order, in Bowl() Method");
        }

        try
        {
            m_scoreDisplay.FillFrames(ScoreManager.ScoreCumulative(m_rolls));
            m_scoreDisplay.FillRolls(m_rolls);
        }
        catch
        {
            Debug.LogWarning("Sir Bhanu, FillRollCard() Method failed for some reason");
        }
	}
}
