namespace StateMachine.PlayerState
{
    public class IdleState : BaseState
    {
        private bool isIdle = false;
        public bool GetIsIdle => isIdle;
        private const string IDLE = "Idle";

        public override void Enter(object obj)
        {
            if (SetActor(obj) == false) return;
            Idle();
        }
    

        public override void Update(object obj)
        {
            if (_actor == null) return;
            if (IsCurrentStateThis() == false) return;
        }

        public override void Exit(object obj)
        {
            base.Exit(_actor);
            StopIdle();
        }
    
        private void Idle()
        {
            isIdle = true;
            RunAnimation(IDLE, isIdle);
        }

        private void StopIdle()
        {
            isIdle = false;
            RunAnimation(IDLE, false);
        }
    }
}