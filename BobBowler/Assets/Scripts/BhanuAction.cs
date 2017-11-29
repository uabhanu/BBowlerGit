using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuAction
{
    int bowl = 1;
    int[] bowls = new int[21];

    public enum Action{TIDY , RESET , ENDTURN , ENDGAME};

	public Action Bowl(int pins)
    {
        if(pins < 0 || pins > 10){throw new UnityException("Sir Bhanu, Pins count is not equal to 10");}

        bowls[bowl - 1] = pins;

        if(bowl == 21)
        {
            return Action.ENDGAME;
        }

        if(bowl >= 19 && Bowl21Awarded())
        {
            bowl += 1;
            return Action.RESET;
        }

        else if(bowl == 20 && !Bowl21Awarded())
        {
            return Action.ENDGAME;
        }

        if(pins == 10)
        {
            bowl += 2;
            return Action.ENDTURN;
        }

        if(bowl % 2 != 0)
        {
            bowl += 1;
            return Action.TIDY;
        }

        else if(bowl % 2 == 0)
        {
            bowl += 1;
            return Action.ENDTURN;
        }

        throw new UnityException("Sir Bhanu, not sure what action to return");
    }

    bool Bowl21Awarded()
    {
        return(bowls[19-1] + bowls[20-1] >= 10);
    }
}
