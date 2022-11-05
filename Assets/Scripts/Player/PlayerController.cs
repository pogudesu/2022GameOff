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
        public float hori;
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
            if(currentState != null)
                currentState.Update(this);
            controllerState = _sidescrollerController.currentControllerState;
            if (controllerState != ControllerState.Grounded)
            {
                currentState?.ChangeState(_airState);
            }
        }

        private void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            bool isJumpPressed = Input.GetKey(KeyCode.Space);
            //
            // _animator.SetBool("Jump", isJumpPressed);
            // _animator.SetBool("IsGround", isGounded);
            // _animator.SetFloat("Horizontal", horizontal);
            // if (horizontal != 0f && isGounded)
            // {
            //     _animator.SetBool("Running", true);
            // }
            // else
            // {
            //     _animator.SetBool("Running", false);
            // }
            hori = horizontal;
            UpdatePlayerMovement(horizontal, isJumpPressed);
        }

        private void UpdatePlayerMovement(float horizontal, bool isJumpPressed)
        {
            if (isJumpPressed)
            {
                currentState.ChangeState(_airState);
            }
            if (horizontal != 0)
            {
                if (_airState.OnAir)
                {
                    nextState = _moveState;
                }
                else
                {
                    currentState.ChangeState(_moveState);
                }
            }
            else
            {
                if (_airState.OnAir)
                {
                    nextState = _idleState;
                }
                else
                {
                    currentState.ChangeState(_idleState);
                }
                
            }
            _playerInputMovement.PlayerMove(horizontal, 0, isJumpPressed);
        }
    }
}