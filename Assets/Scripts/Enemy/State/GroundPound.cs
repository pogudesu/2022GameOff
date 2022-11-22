using StateMachine.PlayerState;
using UnityEngine;

namespace Enemy.State
{
    public class GroundPound : BaseState
    {
        public bool IsGroundPoundState { get; set; }
        public int animationName = Animator.StringToHash("GroundPound");
        public override void Enter(object obj)
        {
            if (SetActor(obj) == false) return;
            base.Enter(obj);
            IsGroundPoundState = true;
            _actor._animator.SetTrigger(animationName);
        }

        public override void Update(object obj)
        {
            base.Update(obj);
        }

        public override void Exit(object obj)
        {
            IsGroundPoundState = false;
            base.Exit(obj);
        }
    }
}