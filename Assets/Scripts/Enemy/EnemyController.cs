using System;
using StateMachine.Data;
using StateMachine.PlayerState;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : ActorData
    {
        private IdleState _idleState = new IdleState();
        private AttackState iceAttack = new AttackState();

        private void Start()
        {
            currentState = _idleState;
            currentState.Enter(this);
        }
    }
}