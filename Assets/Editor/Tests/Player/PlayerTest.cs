using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Player;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    private ActorInputMovement _actorInputMovement;

    [SetUp]
    public void SetUpPlayer()
    {
        _actorInputMovement = new ActorInputMovement();
    }
    [Test]
    [TestCase(1, 0, 1)]
    [TestCase(-1, 0, -1)]
    [TestCase(0, 3, 0)]
    public void PlayerMovementHorizontalTest(float horizontal, float vertical, float expected)
    {
        _actorInputMovement.ActorMove(horizontal,vertical,false);
        Assert.AreEqual(expected, _actorInputMovement.GetHorizontalMovementInput());
    }
    
    [Test]
    [TestCase(1, 1, 0)]
    [TestCase(1, -1, 0)]
    [TestCase(-1, 0, 0)]
    [TestCase(0, 3, 0)]
    public void PlayerMovementVerticalTest(float horizontal, float vertical, float expected)
    {
        _actorInputMovement.ActorMove(horizontal,vertical,false);
        Assert.AreEqual(expected, _actorInputMovement.GetVerticalMovementInput());
    }
    [Test]
    [TestCase(true, true)]
    [TestCase(false, false)]
    public void PlayerMovementJumpTest(bool isJumping, bool expected)
    {
        _actorInputMovement.ActorMove(0,0,isJumping);
        Assert.That(expected == _actorInputMovement.IsJumpKeyPressed(), " Is Jump pressed");
    }
}
