using System;
using System.Collections;
using System.Collections.Generic;
using CMF;
using NUnit.Framework;
using State.Interface;
using StateMachine.Data;
using StateMachine.PlayerState;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class StateMachineTest
{
    // public IStateable state;
    public MoveState moveState;
    public IdleState idleState;
    public AirState airState;
    public ActorData actor;
    
    [SetUp]
    public void StateMachineSetUp()
    {
        GameObject gameObject = new GameObject();
        Animator anim = gameObject.AddComponent<Animator>();
        actor = gameObject.AddComponent<ActorData>();

        actor._animator = anim;
        actor.actorGameObject = gameObject;
        moveState = new MoveState();
        idleState = new IdleState();
        airState = new AirState();
        actor.currentState = idleState;
        actor.currentState.Enter(actor);
    }

    [Test]
    public void TestIdleStateToRunning()
    {
        if (actor.currentState != null)
        {
            actor.currentState = idleState;
            actor.currentState.Update(actor);
            actor.currentState.ChangeState(moveState);
        }
        Assert.AreEqual(true, moveState.GetIsRunning);
    }

    [Test]
    public void TestIdleStateToFalseWhileRunning()
    {
        actor.currentState.ChangeState(idleState);
        actor.currentState.ChangeState(moveState);
        Assert.AreEqual(false, idleState.GetIsIdle);
    }

    [Test]
    public void TestRunningToIdle()
    {
        actor.currentState.ChangeState(idleState);
        actor.currentState.ChangeState(moveState);
        actor.currentState.ChangeState(idleState);
        Assert.AreEqual(true, idleState.GetIsIdle);
    }

    [Test]
    public void TestAirJump()
    {
        actor.currentState.ChangeState(airState);
        actor.controllerState = ControllerState.Jumping;
        Assert.AreEqual(true, airState.IsJumping);
    }
    [Test]
    public void TestAirFalling()
    {
        actor.currentState.ChangeState(airState);
        actor.controllerState = ControllerState.Falling;
        Assert.AreEqual(true, airState.IsFalling);
    }
    
    [Test]
    public void TestAirRising()
    {
        actor.currentState.ChangeState(airState);
        actor.controllerState = ControllerState.Rising;
        Assert.AreEqual(true, airState.IsRising);
    }
    
    [Test]
    public void TestAirGrounded()
    {
        actor.currentState.ChangeState(airState);
        actor.controllerState = ControllerState.Grounded;
        Assert.AreEqual(true, airState.IsGrounded);
    }
    
    [Test]
    public void TestAirLandingWithHorizontalMovementPressed()
    {
        actor.currentState.ChangeState(airState);
        actor.controllerState = ControllerState.Falling;
        actor.nextState = moveState;
        actor.currentState.Update(actor);
        actor.controllerState = ControllerState.Grounded;
        actor.currentState.Update(actor);
        Assert.AreEqual(true, moveState.GetIsRunning);
    }


}




