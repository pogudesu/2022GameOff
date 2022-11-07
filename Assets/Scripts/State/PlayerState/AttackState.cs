using PlayerGun;
using State.Interface;
using UnityEngine;

namespace StateMachine.PlayerState
{
    public class AttackState : BaseState, IShot
    {
        private bool _isAttackState = false;
        public bool IsAttackState => _isAttackState;
        public IGunnable gun;
        
        public override void Enter(object obj)
        {
            if (SetActor(obj) == false) return;
            if (gun.GetBullet() == 0)
            {
                ReturnToPreviousState();
                return;
            }
            base.Enter(_actor);
            _isAttackState = true;
            _actor._animator.SetTrigger(gun.GetAnimationName());
        }

        private void ReturnToPreviousState()
        {
            if (_actor.previousState != null)
            {
                _actor.previousState.Enter(_actor);
            }
        }

        public override void Update(object obj)
        {
            base.Update(obj);
        }

        public override void Exit(object obj)
        {
            _isAttackState = false;
            base.Exit(obj);
        }

        public override void ChangeState(IStateable state)
        {
            return;
        }

        public void Shot()
        {

        }
        
        public void Interupt()
        {
            ReturnToPreviousState();
        }

        public void SetGun(IGunnable newGun)
        {
            if (this.gun == null)
                this.gun = newGun;
        }

        public bool IsCurrentlyInAttackState()
        {
            return IsAttackState;
        }
    }
}