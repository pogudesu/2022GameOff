using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Player;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    private PlayerInputMovement _playerInputMovement;

    [SetUp]
    public void SetUpPlayer()
    {
        _playerInputMovement = new PlayerInputMovement();
    }
    [Test]
    [TestCase(1, 0, 1)]
    [TestCase(-1, 0, -1)]
    [TestCase(0, 3, 0)]
    public void PlayerMovementHorizontalTest(float horizontal, float vertical, float expected)
    {
        _playerInputMovement.PlayerMove(horizontal,vertical,false);
        Assert.AreEqual(expected, _playerInputMovement.GetHorizontalMovementInput());
    }
    
    [Test]
    [TestCase(1, 1, 0)]
    [TestCase(1, -1, 0)]
    [TestCase(-1, 0, 0)]
    [TestCase(0, 3, 0)]
    public void PlayerMovementVerticalTest(float horizontal, float vertical, float expected)
    {
        _playerInputMovement.PlayerMove(horizontal,vertical,false);
        Assert.AreEqual(expected, _playerInputMovement.GetVerticalMovementInput());
    }
    [Test]
    [TestCase(true, true)]
    [TestCase(false, false)]
    public void PlayerMovementJumpTest(bool isJumping, bool expected)
    {
        _playerInputMovement.PlayerMove(0,0,isJumping);
        Assert.That(expected == _playerInputMovement.IsJumpKeyPressed(), " Is Jump pressed");
    }
}
