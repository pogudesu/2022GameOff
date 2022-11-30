using Audio;
using State.Interface;

namespace StateMachine.PlayerState
{
public class AirState : BaseState
{
    private bool isOnAir = false;
    private const string JUMP = "Jump";
    private const string FALLING = "Falling";
    private const string LANDING = "Landing";
    private const string GROUNDED = "IsGrounded";
    
    public bool OnAir => isOnAir;
    public bool IsGrounded => _actor.controllerState == ControllerState.Grounded;
    public bool IsRising => _actor.controllerState == ControllerState.Rising;
    public bool IsFalling => _actor.controllerState == ControllerState.Falling || _actor.controllerState == ControllerState.Rising;
    public bool IsJumping => _actor.controllerState == ControllerState.Jumping;
    public ControllerState airState { get; private set; }
    private bool isPlayedJump = false;
    private bool isPlayedLanding = false;

    public override void Enter(object obj)
    {
        if (SetActor(obj) == false) return;
        base.Enter(_actor);
        isOnAir = true;
        _actor.currentJumpAvailable -= 1;
        if (_actor.currentJumpAvailable < 0)
            _actor.currentJumpAvailable = 0;
        isPlayedJump = false;
        isPlayedLanding = false;
        // if(_actor.controllerState == ControllerState.Grounded || _actor.controllerState == ControllerState.Jumping)
        //     Jump();
        // else if(_actor.controllerState == ControllerState.Falling || _actor.controllerState == ControllerState.Rising)
        //     Falling();
    }

    public override void Update(object obj)
    {
        RunAnimation(JUMP, IsJumping);
        RunAnimation(FALLING, IsFalling || IsRising);
        RunAnimation(LANDING, IsGrounded);
        // RunAnimation(GROUNDED, IsGrounded);
        ExecuteEachAirState();
        if (IsGrounded == false) return;
        ChangeToNextAvailableState();
    }

    private void ExecuteEachAirState()
    {
        if (IsJumping)
        {
            Jump();
        }else if (IsFalling)
        {
            Falling();
        }else if (IsGrounded)
        {
            Landing();
        }
    }

    public override void ChangeState(IStateable state)
    {
        if (state is HitState)
        {
            StopAnimation();
            base.ChangeState(state);
        }
        else
        {
            base.ChangeState(state);
        }
    }

    public void StopAnimation()
    {
        RunAnimation(JUMP, false);
        RunAnimation(FALLING, false);
        RunAnimation(LANDING, false);
    }

    private void ChangeToNextAvailableState()
    {
        Exit(_actor);
        // MoveState moveState = (MoveState)_actor.nextState;
        if (_actor.nextState != null)
        {
            ChangeState(_actor.nextState);
        }
    }

    public override void Exit(object obj)
    {
        // StopAnimation();
        isOnAir = false;
        base.Exit(obj);
        if(IsGrounded)
            SFXController.PlayJump();
    }
    
    private void Jump()
    {

            
    }

    private void Falling()
    {
        // play when run off the clip
        // Play after jump
    }

    private void Landing()
    {
        //Player Animation landing
        // if (isPlayedLanding == false)
        // {
        //     SFXController.PlayLanding();
        //     isPlayedLanding = true;
        // }
        // SFXController.PlayLanding();
    }
}
}