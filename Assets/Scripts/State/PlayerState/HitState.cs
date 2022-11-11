using State.Interface;

namespace StateMachine.PlayerState
{
    public class HitState: BaseState
    {
        public bool IsOnHitState { get; set; }
        public bool IsOnAirStateHit { get; set; }
        public override void Enter(object obj)
        {
            base.Enter(obj);
            if (SetActor(obj) == false) return;
            Hit();
        }

        private void Hit()
        {
            IsOnHitState = true;
            if (_actor.previousState is AirState)
            {
                _actor._animator.SetTrigger("Knockdown");
                IsOnAirStateHit = true;
            }
            else
            {
                _actor._animator.SetTrigger("Hit");
            }
        }

        public override void Update(object obj)
        {
            base.Update(obj);
        }

        public override void ChangeState(IStateable state)
        {
            return;
        }

        public override void Exit(object obj)
        {
            IsOnHitState = false;
            IsOnAirStateHit = false;
            base.Exit(obj);
        }
    }
}