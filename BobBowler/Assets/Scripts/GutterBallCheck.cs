using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GutterBallCheck : MonoBehaviour 
{
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
            m_pinSetter.m_standingPinsDisplayOutlineColour = Color.red;
            m_pinSetter.m_standingPinsDisplayOutline.effectColor = m_pinSetter.m_standingPinsDisplayOutlineColour;
            m_pinSetter.m_standingPinsTextOutlineColour = Color.red;
            m_pinSetter.m_standingPinsTextOutline.effectColor = m_pinSetter.m_standingPinsTextOutlineColour;
            m_pinSetter.PinsStandingAndSettle();
        }
    }
}
