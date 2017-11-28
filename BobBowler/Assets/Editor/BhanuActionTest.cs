using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class BhanuActionTest 
{
    BhanuAction.Action m_endTurn = BhanuAction.Action.ENDTURN;

	[Test]
	public void T00PassingTest() 
    {
		Assert.AreEqual(1 , 1);
	}

    [Test]
    public void T01OneStrikeReturnsEndTurn() 
    {
        BhanuAction bhanuAction = new BhanuAction();

        Assert.AreEqual(m_endTurn , bhanuAction.Bowl(10));
    }
}
