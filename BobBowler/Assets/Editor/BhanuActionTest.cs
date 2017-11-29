using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class BhanuActionTest 
{
    BhanuAction m_bhanuAction;
    BhanuAction.Action m_endGame = BhanuAction.Action.ENDGAME;
    BhanuAction.Action m_endTurn = BhanuAction.Action.ENDTURN;
    BhanuAction.Action m_reset = BhanuAction.Action.RESET;
    BhanuAction.Action m_tidy = BhanuAction.Action.TIDY;

    [SetUp]
    public void Setup()
    {
        m_bhanuAction = new BhanuAction();
    }

	[Test]
	public void T00PassingTest() 
    {
		Assert.AreEqual(1 , 1);
	}

    [Test]
    public void T01OneStrikeReturnsEndTurn() 
    {
        Assert.AreEqual(m_endTurn , m_bhanuAction.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy() 
    {
        Assert.AreEqual(m_tidy , m_bhanuAction.Bowl(8));
    }

    [Test]
    public void T03Bowl28SpareReturnsEndTurn() 
    {
        m_bhanuAction.Bowl(8);
        Assert.AreEqual(m_endTurn , m_bhanuAction.Bowl(2));
    }

    [Test]
    public void T04CheckResetAtStrikeInLastFrame() 
    {
        int[] rolls =  {1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1};

        foreach(int roll in rolls)
        {
            m_bhanuAction.Bowl(roll);
        }

        Assert.AreEqual(m_reset , m_bhanuAction.Bowl(10));
    }

    [Test]
    public void T05CheckResetAtSpareInLastFrame() 
    {
        int[] rolls =  {1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1};

        foreach(int roll in rolls)
        {
            m_bhanuAction.Bowl(roll);
        }

        m_bhanuAction.Bowl(1);
        Assert.AreEqual(m_reset , m_bhanuAction.Bowl(9));
    }

    [Test]
    public void T06YoutubeRollsEndInEndGame() 
    {
        int[] rolls =  {8,2 , 7,3 , 3,4 , 10 , 2,8 , 10 , 10 , 8,0 , 10 , 8,2};

        foreach(int roll in rolls)
        {
            m_bhanuAction.Bowl(roll);
        }

        Assert.AreEqual(m_endGame , m_bhanuAction.Bowl(9));
    }

    [Test]
    public void T07EndGameAtBowl20() 
    {
        int[] rolls =  {1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1};

        foreach(int roll in rolls)
        {
            m_bhanuAction.Bowl(roll);
        }

        Assert.AreEqual(m_endGame , m_bhanuAction.Bowl(1));
    }

    [Test]
    public void T08NathanBowlIndexTest() 
    {
        int[] rolls =  {0 , 10 , 5};

        foreach(int roll in rolls)
        {
            m_bhanuAction.Bowl(roll);
        }

        Assert.AreEqual(m_endTurn , m_bhanuAction.Bowl(1));
    }

    [Test]
    public void T09Dondi10thFrameTurkey() 
    {
        int[] rolls =  {1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1 , 1,1};

        foreach(int roll in rolls)
        {
            m_bhanuAction.Bowl(roll);
        }

        Assert.AreEqual(m_reset , m_bhanuAction.Bowl(10));
        Assert.AreEqual(m_reset , m_bhanuAction.Bowl(10));
        Assert.AreEqual(m_endGame , m_bhanuAction.Bowl(10));
    }

    [Test]
    public void T10ZeroOneGivesEndTurn() 
    {
        m_bhanuAction.Bowl(0);
        Assert.AreEqual(m_endTurn , m_bhanuAction.Bowl(1));
    }
}
