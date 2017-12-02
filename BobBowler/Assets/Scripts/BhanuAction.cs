using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BhanuAction 
{
	public enum Action {TIDY , RESET , ENDTURN , ENDGAME , UNDEFINED};
	
	public static Action NextAction (List<int> rolls) 
    {
        Action nextAction = Action.UNDEFINED;
		
        for(int i = 0; i < rolls.Count; i++) // Step through rolls
        { 
			
			if(i == 20) 
            {
                nextAction = Action.ENDGAME;
			} 

            else if( i >= 18 && rolls[i] == 10 ) // Handle last-frame special cases
            {
                nextAction = Action.RESET;
			} 

            else if(i == 19) 
            {
				if(rolls[18] == 10 && rolls[19] == 0) 
                {
                    nextAction = Action.TIDY;
				} 

                else if(rolls[18] + rolls[19] == 10) 
                {
                    nextAction = Action.RESET;
				} 

                else if(rolls [18] + rolls[19] >= 10)   // Roll 21 awarded
                {
                    nextAction = Action.TIDY;
				} 

                else 
                {
                    nextAction = Action.ENDGAME;
				}

			} 

            else if(i % 2 == 0)  // First bowl of frame
            {
				if(rolls[i] == 10) 
                {
					rolls.Insert(i , 0); // Insert virtual 0 after strike
                    nextAction = Action.ENDTURN;
				} 
                else 
                {
                    nextAction = Action.TIDY;
				}
			} 

            else  // Second bowl of frame
            {
                nextAction = Action.ENDTURN;
			}
		}
		
		return nextAction;
	}
}