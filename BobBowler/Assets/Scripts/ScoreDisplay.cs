using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour 
{
    [SerializeField] Text[] m_frameTexts , m_rollTexts; 

    public void FillFrames(List<int> frames)
    {
        for(int i = 0; i < frames.Count; i++)
        {
            m_frameTexts[i].text = frames[i].ToString();
        }
    }

    public void FillRolls(List<int> rolls)
    {
        string scoresString = FormatRolls(rolls);

        for(int i = 0; i < scoresString.Length; i++)
        {
            m_rollTexts[i].text = scoresString[i].ToString();
        }
    }

    public static string FormatRolls(List<int> rolls)
    {
        string output = "";

        for(int i = 0; i < rolls.Count; i++)
        {
            int box = output.Length + 1; //Score Box 1 to 21

            if(rolls[i] == 0)
            {
                output += "-"; //Always enter '0' as '-'
            }

            else if((box % 2 == 0 || box == 21) && rolls[i - 1] + rolls[i] == 10)
            {
                output += "/"; //Spare anywhere
            }

            else if(box >= 19 && rolls[i] == 10)
            {
                output += "X"; //Strike in Last Frame
            }

            else if(rolls[i] == 10)
            {
                output += "X "; //Strike in frames 1 to 9
            }

            else
            {
                output += rolls[i].ToString(); //Normal 1 to 9 Bowl
            }
        }

        return output;
    }
}   
