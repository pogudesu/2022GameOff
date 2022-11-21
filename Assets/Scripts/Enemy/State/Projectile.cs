using StateMachine.PlayerState;
using UnityEngine;

namespace Enemy.State
{
    public class Projectile : BaseState
    {
        public bool IsProjectileState { get; set; }
        public int projectileAnimation = Animator.StringToHash("Projectile");
        public override void Enter(object obj)
        {
            if (SetActor(obj) == false) return;
            base.Enter(obj);
            IsProjectileState = true;
            _actor._animator.SetTrigger(projectileAnimation);
        }

        public override void Update(object obj)
        {
            base.Update(obj);
        }

        public override void Exit(object obj)
        {
            IsProjectileState = false;
            base.Exit(obj);
        }
    }
}