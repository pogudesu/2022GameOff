namespace StateMachine.PlayerState
{
    public class MoveState : BaseState
    {
        private bool isRunning = false;
        public bool GetIsRunning => isRunning;
        private const string RUNNING = "Running";
    
        public override void Enter(object obj)
        {
            if (SetActor(obj) == false) return;
            isRunning = true;
            RunAnimation(RUNNING, isRunning);
        }

        public override void Update(object obj)
        {
            if (_actor == null) return;
            if (IsCurrentStateThis() == false) return;
        }
    
        private void StopMove()
        {
            isRunning = false;
            RunAnimation(RUNNING, isRunning);
        }


        public override void Exit(object obj)
        {
            base.Exit(_actor);
            StopMove();
        }
    }
}