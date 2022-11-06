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
        private bool IsPistolState => _pistolState.IsAttackState;
        private bool IsSniperState => _sniperState.IsAttackState;
        private bool IsDualPistolState => _dualPistolState.IsAttackState;
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
            float horizontalMovement = Input.GetAxis("Horizontal");
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
                UpdatePlayerMovement(horizontalMovement, isJumpPressed);
            }
        }
        
        private void StartAttackWithPistol()
        {
            if (IsStateNotAvailableToShoot() == false) return;
            ChangeState(_pistolState);
        }
        private void StartAttackWithSniper()
        {
            if (IsStateNotAvailableToShoot() == false) return;
            ChangeState(_sniperState);
        }
        
        private void StartAttackWithDualPistol()
        {
            if (IsStateNotAvailableToShoot() == false) return;
            ChangeState(_dualPistolState);
        }

        private bool IsStateNotAvailableToShoot()
        {
            return IsIdleState || IsMoveState ;
        }

        private void UpdatePlayerMovement(float horizontal, bool isJumpPressed)
        {
            bool isMoving = horizontal != 0;
            if (isMoving)
            {
                InitializedMoveState();
            }
            else
            {
                InitializedIdleState();
            }
            if (IsCurrentlyInAttackState())
            {
                StopMoving();
                return;
            }
            EnterJumpState(isJumpPressed);
            _playerInputMovement.PlayerMove(horizontal, 0, isJumpPressed);
        }

        private void StopMoving()
        {
            _playerInputMovement.PlayerMove(0, 0, false);
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
        
        private bool IsCurrentlyInAttackState()
        {
            return IsPistolState || IsSniperState || IsDualPistolState;
        }


        #region ############################ Animation Event ###################################

        public void AttackAnimationEnd()
        {
            Debug.Log("Attack End");
            currentState.Exit(this);
            currentState = _idleState;
            currentState.Enter(this);
        }

        public void FireShot()
        {
            Debug.Log("Attack");
            // Attack handler job to do logic
        }
        
        
        //          This Animation is for Dual Pistol           //
        public void BothGunFire()
        {
            Debug.Log("BothGunFire");
            // Attack handler job to do logic
        }
        
        public void GunOneFire()
        {
            Debug.Log("GunOneFire");
            // Attack handler job to do logic
        }
        
        public void GunTwoFire()
        {
            Debug.Log("GunTwoFire");
            // Attack handler job to do logic
        }

        #endregion
    }
}