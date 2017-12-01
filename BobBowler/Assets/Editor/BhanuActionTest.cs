using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BhanuActionTest 
{
    List<int> m_pinFalls;
    BhanuAction.Action m_endGame = BhanuAction.Action.ENDGAME;
    BhanuAction.Action m_endTurn = BhanuAction.Action.ENDTURN;
    BhanuAction.Action m_reset = BhanuAction.Action.RESET;
    BhanuAction.Action m_tidy = BhanuAction.Action.TIDY;

    [SetUp]
    public void Setup()
    {
        m_pinFalls = new List<int>();
    }

	[Test]
	public void T00PassingTest() 
    {
		Assert.AreEqual(1 , 1);
	}

    [Test]
    public void T01OneStrikeReturnsEndTurn() 
    {
        m_pinFalls.Add(10);
        Assert.AreEqual(m_endTurn , BhanuAction.NextAction(m_pinFalls));
    }

    [Test]
    public void T02Bowl8ReturnsTidy() 
    {
        m_pinFalls.Add(8);
        Assert.AreEqual(m_tidy , BhanuAction.NextAction(m_pinFalls));
    }

    [Test]
    public void T03Bowl28SpareReturnsEndTurn() 
    {
        int[] rolls = {2 , 8};
        Assert.AreEqual(m_endTurn , BhanuAction.NextAction(rolls.ToList()));
    }

    [Test]
    public void T04CheckResetAtStrikeInLastFrame() 
    {
        int[] rolls =  {1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 10};
        Assert.AreEqual(m_reset , BhanuAction.NextAction(rolls.ToList()));
    }

    [Test]
    public void T05CheckResetAtSpareInLastFrame() 
    {
        int[] rolls =  {1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,9};
        Assert.AreEqual(m_reset , BhanuAction.NextAction(rolls.ToList()));
    }

    [Test]
    public void T06YoutubeRollsEndInEndGame() 
    {
        int[] rolls =  {8,2 , 7,3 , 3,4 , 10 , 2,8 , 10 , 10 , 8,0 , 10 , 8,2 , 9};
        Assert.AreEqual(m_endGame , BhanuAction.NextAction(rolls.ToList()));
    }

    [Test]
    public void T07EndGameAtBowl20() 
    {
        int[] rolls =  {1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1 , 1};
        Assert.AreEqual(m_endGame , BhanuAction.NextAction(rolls.ToList()));
    }

    [Test]
    public void T08DarylBowl20Test() 
    {
        int[] rolls =  {1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 10 , 5};
        Assert.AreEqual(m_tidy , BhanuAction.NextAction(rolls.ToList()));
    }

    [Test]
    public void T09BensBowl20Test() 
    {
        int[] rolls =  {1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 10 , 0};
        Assert.AreEqual(m_tidy , BhanuAction.NextAction(rolls.ToList()));
    }

    [Test]
    public void T10NathanBowlIndexTest() 
    {
        int[] rolls =  {0 , 10 , 5 , 1};
        Assert.AreEqual(m_endTurn , BhanuAction.NextAction(rolls.ToList()));
    }

    [Test]
    public void T11Dondi10thFrameTurkey() 
    {
        int[] rolls =  {1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 10 , 10 , 10};
        Assert.AreEqual(m_endGame , BhanuAction.NextAction(rolls.ToList()));
    }

    [Test]
    public void T12ZeroOneGivesEndTurn() 
    {
        int[] rolls =  {0 , 1};
        Assert.AreEqual(m_endTurn , BhanuAction.NextAction(rolls.ToList()));
    }

    [Test]
    public void T13BhanuBowl7ReturnsTidy() 
    {
        m_pinFalls.Add(7);
        Assert.AreEqual(m_tidy , BhanuAction.NextAction(m_pinFalls));
    }
}
