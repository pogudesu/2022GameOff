using System;
using CMF;
using PlayerGun;
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
        private AttackState _pistolState = new AttackState();
        private AttackState _sniperState = new AttackState();
        private AttackState _dualPistolState = new AttackState();
        private bool IsAirState => _airState.OnAir;
        private bool IsMoveState => _moveState.GetIsRunning;
        private bool IsIdleState => _idleState.GetIsIdle;
        private bool IsCurrentStateNotNull => currentState != null;
        private bool IsNotGrounded => controllerState != ControllerState.Grounded;

        public Gun pistolGun;
        public Gun sniperGun;
        public Gun dualPistolGun;
        private void Awake()
        {
            _playerInputMovement = GetComponent<PlayerInputMovement>();
            _animator = GetComponent<Animator>();
            _sidescrollerController = GetComponent<SidescrollerController>();
            _sidescrollerController.SetInput(_playerInputMovement);
            
            _pistolState.SetGun(pistolGun);
            _sniperState.SetGun(sniperGun);
            _dualPistolState.SetGun(dualPistolGun);
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
            bool IsPistolAttackPressed = Input.GetMouseButton(0);
            bool IsSniperAttackPressed = Input.GetMouseButton(1);
            bool IsDualPistolPressed = Input.GetKeyDown(KeyCode.Q);
            float horizontal = Input.GetAxis("Horizontal");
            bool isJumpPressed = Input.GetKey(KeyCode.Space);

            if (IsPistolAttackPressed)
            {
                StartAttackWithPistol();
            }else if (IsSniperAttackPressed)
            {
                StartAttackWithSniper();
            }else if (IsDualPistolPressed)
            {
                StartAttackWithDualPistol();
            }
            else
            {
                UpdatePlayerMovement(horizontal, isJumpPressed);
            }
        }

        private void StartAttackWithDualPistol()
        {
            if (IsStateNotAvailableToShoot()) return;
            ChangeState(_pistolState);
        }

        private bool IsStateNotAvailableToShoot()
        {
            return IsIdleState == false || IsMoveState == false;
        }

        private void StartAttackWithSniper()
        {
            if (IsStateNotAvailableToShoot()) return;
            ChangeState(_sniperState);
        }

        private void StartAttackWithPistol()
        {
            if (IsStateNotAvailableToShoot()) return;
            ChangeState(_dualPistolState);
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