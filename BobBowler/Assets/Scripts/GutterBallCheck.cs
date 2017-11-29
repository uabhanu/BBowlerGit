using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GutterBallCheck : MonoBehaviour 
{
    float m_gutterBallCheckBallResetTime = 1.25f;
    PinSetter m_pinSetter;

    void Start() 
    {
        m_pinSetter = FindObjectOfType<PinSetter>();	
	}
	
    void OnTriggerExit(Collider tri)
    {
        if(tri.gameObject.tag.Equals("Ball"))
        {
            m_pinSetter.m_ballOutOfPlay = true;
            m_pinSetter.m_ballResetTime = m_gutterBallCheckBallResetTime;
            m_pinSetter.m_standingPinsDisplayOutlineColour = Color.red;
            m_pinSetter.m_standingPinsDisplayOutline.effectColor = m_pinSetter.m_standingPinsDisplayOutlineColour;
            m_pinSetter.m_standingPinsTextOutlineColour = Color.red;
            m_pinSetter.m_standingPinsTextOutline.effectColor = m_pinSetter.m_standingPinsTextOutlineColour;
            m_pinSetter.StartCoroutine("BallResetRoutine");
        }
    }
}
