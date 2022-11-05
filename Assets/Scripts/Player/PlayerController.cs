using System;
using CMF;
using State.Interface;
using StateMachine.Data;
using StateMachine.PlayerState;
using UnityEngine;

namespace Player
{
    public class PlayerController : ActorData
    {
        private PlayerInputMovement _playerInputMovement;
        private SidescrollerController _sidescrollerController;

        private MoveState _moveState = new MoveState();
        private IdleState _idleState = new IdleState();
        private AirState _airState = new AirState();
        private bool IsAirState => _airState.OnAir;
        private bool IsMoveState => _moveState.GetIsRunning;
        private bool IsIdleState => _idleState.GetIsIdle;
        private bool IsCurrentStateNotNull => currentState != null;
        private bool IsNotGrounded => controllerState != ControllerState.Grounded;
        private void Awake()
        {
            _playerInputMovement = GetComponent<PlayerInputMovement>();
            _animator = GetComponent<Animator>();
            _sidescrollerController = GetComponent<SidescrollerController>();
            _sidescrollerController.SetInput(_playerInputMovement);
        }

        private void Start()
        {
            actorGameObject = this.gameObject;
            currentState = _idleState;
            currentState.Enter(this);
        }

        private void Update()
        {
            if(IsCurrentStateNotNull)
                currentState.Update(this);
            UpdateControlState();
            if (IsNotGrounded)
            {
                ChangeState(_airState);
            }
        }

        private void UpdateControlState()
        {
            controllerState = _sidescrollerController.currentControllerState;
        }

        private void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            bool isJumpPressed = Input.GetKey(KeyCode.Space);
            UpdatePlayerMovement(horizontal, isJumpPressed);
        }

        private void UpdatePlayerMovement(float horizontal, bool isJumpPressed)
        {
            EnterJumpState(isJumpPressed);
            bool isMoving = horizontal != 0;
            if (isMoving)
            {
                InitializedMoveState();
            }
            else
            {
                InitializedIdleState();
            }
            _playerInputMovement.PlayerMove(horizontal, 0, isJumpPressed);
        }

        private void InitializedIdleState()
        {
            if (IsAirState)
            {
                nextState = _idleState;
            }
            else
            {
                if (IsIdleState) return;
                ChangeState(_idleState);
            }
        }

        private void InitializedMoveState()
        {
            if (IsAirState)
            {
                nextState = _moveState;
            }
            else
            {
                if (IsMoveState) return;
                ChangeState(_moveState);
            }
        }

        private void EnterJumpState(bool isJumpPressed)
        {
            if (isJumpPressed)
            {
                ChangeState(_airState);
            }
        }

        private void ChangeState(IStateable state)
        {
            currentState.ChangeState(state);
        }
    }
}