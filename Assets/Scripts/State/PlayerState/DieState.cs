using State.Interface;

namespace StateMachine.PlayerState
{

    public class DieState : BaseState
    {
        public bool IsDiedState { get; set; }
        public override void Enter(object obj)
        {
            if(SetActor(obj) == false) return;
            Die();
        }

        private void Die()
        {
            _actor._animator.SetTrigger("Died");
            _actor.isActorDied = true;
            IsDiedState = true;
            if (_actor.onDied != null)
                _actor.onDied.Invoke();
        }

        public override void Update(object obj)
        {
            return;
        }

        public override void ChangeState(IStateable state)
        {
            return;
        }

        public override void Exit(object obj)
        {
            return;
            base.Exit(obj);
        }
    }
}