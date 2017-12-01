using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuAction
{
    int bowl = 1;
    int[] bowls = new int[21];

    public enum Action{TIDY , RESET , ENDTURN , ENDGAME};

	Action Bowl(int pins)
    {
        if(pins < 0 || pins > 10){throw new UnityException("Sir Bhanu, Pins count is not equal to 10");}

        bowls[bowl - 1] = pins;

        if(bowl == 21)
        {
            return Action.ENDGAME;
        }

        if(bowl >= 19 && pins == 10)
        {
            bowl ++;
            return Action.RESET;
        }

        else if(bowl == 20)
        {
            bowl++;

            if(bowls[19-1] == 10 && bowls[20-1] == 0)
            {
                return Action.TIDY;
            }

            else if(bowls[19-1] + bowls[20-1] == 10)
            {
                return Action.RESET;
            }

            else if(Bowl21Awarded())
            {
                return Action.TIDY;
            }

            else
            {
                return Action.ENDGAME;
            }
        }

        if(bowl % 2 != 0)
        {
            if(pins == 10)
            {
                bowl += 2;
                return Action.ENDTURN;
            }
            else
            {
                bowl += 1;
                return Action.TIDY;
            }
        }

        else if(bowl % 2 == 0)
        {
            bowl += 1;
            return Action.ENDTURN;
        }

        throw new UnityException("Sir Bhanu, not sure what action to return");
    }

    public static Action NextAction(List<int> pinFalls)
    {
        BhanuAction bhanuAction = new BhanuAction();
        Action currentAction = new Action();

        foreach(int pinFall in pinFalls)
        {
            currentAction = bhanuAction.Bowl(pinFall);
        }

        return currentAction;
    }

    bool Bowl21Awarded()
    {
        return(bowls[19-1] + bowls[20-1] >= 10);
    }
}
