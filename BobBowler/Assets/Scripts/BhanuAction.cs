using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuAction
{
    public enum Action
    {
        TIDY,
        RESET,
        ENDTURN,    
    };

	public Action Bowl(int pins)
    {
        return Action.ENDTURN;
    }
}
