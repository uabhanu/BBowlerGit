using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour 
{
    [SerializeField] Text[] m_frameTexts , m_rollTexts; 
    
	void Start() 
    {
		
	}

	void Update() 
    {
		
	}

    public void FillRollCard(List<int> rolls)
    {
        rolls.Add(11);    
    }
}   
