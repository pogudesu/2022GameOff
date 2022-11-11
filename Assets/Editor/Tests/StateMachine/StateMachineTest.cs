
using Enemy.Data;
using Enemy.State;
using NUnit.Framework;
using PlayerGun;
using State.Interface;
using StateMachine.Data;
using StateMachine.PlayerState;
using UnityEngine;


public class StateMachineTest
{
    // public IStateable state;
    public MoveState moveState;
    public IdleState idleState;
    public AirState airState;
    public AttackState attackState;
    public ActorData actor;
    public Gun gun;

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
        attackState = new AttackState();
        actor.currentState = idleState;
        actor.currentState.Enter(actor);

        gun = ScriptableObject.CreateInstance<Gun>();
    }

    [TearDown]
    public void StatMachinTearDown()
    {
        Object.DestroyImmediate(gun);
    }

    [Test]
    public void Test_Idle_State_To_Running()
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
    public void Test_Idle_State_To_False_While_Running()
    {
        actor.currentState.ChangeState(idleState);
        actor.currentState.ChangeState(moveState);
        Assert.AreEqual(false, idleState.GetIsIdle);
    }

    [Test]
    public void Test_Running_To_Idle()
    {
        actor.currentState.ChangeState(idleState);
        actor.currentState.ChangeState(moveState);
        actor.currentState.ChangeState(idleState);
        Assert.AreEqual(true, idleState.GetIsIdle);
    }

    [Test]
    public void Test_Air_Jump()
    {
        actor.currentState.ChangeState(airState);
        actor.controllerState = ControllerState.Jumping;
        Assert.AreEqual(true, airState.IsJumping);
    }
    [Test]
    public void Test_Air_Falling()
    {
        actor.currentState.ChangeState(airState);
        actor.controllerState = ControllerState.Falling;
        Assert.AreEqual(true, airState.IsFalling);
    }
    
    [Test]
    public void Test_Air_Rising()
    {
        actor.currentState.ChangeState(airState);
        actor.controllerState = ControllerState.Rising;
        Assert.AreEqual(true, airState.IsRising);
    }
    
    [Test]
    public void Test_Air_Grounded()
    {
        actor.currentState.ChangeState(airState);
        actor.controllerState = ControllerState.Grounded;
        Assert.AreEqual(true, airState.IsGrounded);
    }
    
    [Test]
    public void Test_Air_Landing_With_Horizontal_Movement_Pressed()
    {
        actor.currentState.ChangeState(airState);
        actor.controllerState = ControllerState.Falling;
        actor.nextState = moveState;
        actor.currentState.Update(actor);
        actor.controllerState = ControllerState.Grounded;
        actor.currentState.Update(actor);
        Assert.AreEqual(true, moveState.GetIsRunning);
        
    }
    
    #region ##################################### Attack Player State ###################################

    [Test]
    public void Test_Gun_Shot()
    {
        gun.currentNumBullet = 1;
        attackState.SetGun(gun);
        actor.currentState.ChangeState(attackState);
        Assert.AreEqual(true, attackState.IsAttackState);
    }

    [Test]
    public void Test_Gun_Shot_Then_Change_Move_State_Immediately()
    {
        gun.currentNumBullet = 1;
        attackState.SetGun(gun);
        actor.currentState.ChangeState(attackState);
        actor.currentState.ChangeState(moveState);
        Assert.AreEqual(true, attackState.IsAttackState);
    }

    [Test]
    public void Test_Gun_With_EmptyBullets()
    {
        actor.currentState.ChangeState(moveState);
        gun.currentNumBullet = 0;
        attackState.SetGun(gun);
        actor.currentState.ChangeState(attackState);
        Assert.AreEqual(true, moveState.GetIsRunning);
    }

    #endregion
    
    #region ##################################### Attack Enemy State ###################################

    [Test]
    public void Enemy_Ice_Attack()
    {
        AttackEnemyState iceAttackState = new AttackEnemyState();
        EnemyAttackData enemyAttackData = new EnemyAttackData() { animatorName = "Sample" };
        iceAttackState.SetEnemyData(enemyAttackData);
        actor.currentState.Exit(actor);
        actor.currentState = iceAttackState;
        actor.currentState.Enter(actor);
        Assert.AreEqual(true, iceAttackState.IsAttacking);
    }
    #endregion

    #region Died State


    [Test]
    public void Test_Player_Died()
    {
        DieState die = new DieState();
        actor.currentState.ChangeState(die);
        Assert.AreEqual(true, actor.isActorDied);
    }

    [Test]
    public void Test_Player_Died_While_Moving()
    {
        DieState die = new DieState();
        actor.currentState.ChangeState(moveState);
        actor.currentState.ChangeState(die);
        Assert.AreEqual(true, actor.isActorDied);
    }
    
    [Test]
    public void Test_Player_Died_While_On_Attack_State()
    {
        DieState die = new DieState();
        gun.currentNumBullet = 1;
        attackState.SetGun(gun);
        actor.currentState.ChangeState(attackState);
        actor.currentState.ChangeState(die);
        Assert.AreEqual(true, actor.isActorDied);
    }

    #endregion

    
    
    



    

}




