using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSetter : MonoBehaviour 
{
    Animator m_animator;
    GameObject m_pinsPrefabInScene;
    Pin[] m_pins;
    PinCounter m_pinCounter;

    [SerializeField] GameObject m_pinsPrefab;

    public float m_ballResetTime;

	void Start() 
	{
        m_animator = GetComponent<Animator>();
        m_pinCounter = FindObjectOfType<PinCounter>();
        m_pins = FindObjectsOfType<Pin>();
	}
    
	void OnTriggerEnter(Collider tri)
	{
		if(tri.gameObject.tag.Equals("Ball"))
		{
            m_pinCounter.m_ballOutOfPlay = true;
            m_pinCounter.m_standingPinsDisplayOutlineColour = Color.red;
            m_pinCounter.m_standingPinsDisplayOutline.effectColor = m_pinCounter.m_standingPinsDisplayOutlineColour;
            m_pinCounter.m_standingPinsTextOutlineColour = Color.red;
            m_pinCounter.m_standingPinsTextOutline.effectColor = m_pinCounter.m_standingPinsTextOutlineColour;
		}
	}

    public void PerformAction(BhanuAction.Action bhanuAction)
    {
        if(bhanuAction == BhanuAction.Action.TIDY)
        {
            m_animator.SetTrigger("Tidy");
        }

        else if(bhanuAction == BhanuAction.Action.ENDTURN || bhanuAction == BhanuAction.Action.RESET)
        {
            m_animator.SetTrigger("Reset");
            m_pinCounter.Reset();
        }

        else if(bhanuAction == BhanuAction.Action.ENDGAME)
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
        Instantiate(m_pinsPrefab , new Vector3(0f , 0f , 1829f) , Quaternion.identity);
        m_pinsPrefabInScene = GameObject.FindGameObjectWithTag("Pins");

        if(m_pinsPrefabInScene.transform.childCount == 0)
        {
            Destroy(m_pinsPrefabInScene.gameObject);
        }
    }
}
