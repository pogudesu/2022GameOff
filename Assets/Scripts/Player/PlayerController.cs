using System;
using System.Collections;
using CMF;
using PlayerGun;
using State.Interface;
using StateMachine.Data;
using StateMachine.PlayerState;
using UnityEngine;
using Attack;
using EventHandler;
using HealthSystem;
using NaughtyAttributes;
using Obvious.Soap;

namespace Player
{
    public class PlayerController : ActorData
    {
        private ActorInputMovement _actorInputMovement;
        private SidescrollerController _sidescrollerController;
        private EquipingGunController _gunGameObjectHandler;
        private AttackHandler _attackHandler;

        private MoveState _moveState = new MoveState();
        private IdleState _idleState = new IdleState();
        private AirState _airState = new AirState();
        private AttackState _pistolState = new AttackState();
        private AttackState _sniperState = new AttackState();
        private AttackState _dualPistolState = new AttackState();
        private DieState _dieState = new DieState();
        private HitState _hitState = new HitState();
        private bool IsAirState => _airState.OnAir;
        private bool IsMoveState => _moveState.GetIsRunning;
        private bool IsIdleState => _idleState.GetIsIdle;
        private bool IsPistolState => _pistolState.IsAttackState;
        private bool IsSniperState => _sniperState.IsAttackState;
        private bool IsDualPistolState => _dualPistolState.IsAttackState;
        private bool IsCurrentStateNotNull => currentState != null;
        private bool IsNotGrounded => controllerState != ControllerState.Grounded;
        private bool IsHitState => _hitState.IsOnHitState;
        public float durationOfInvincibility = 2f;
        // public LayerMask playerLayerNormal;
        // public LayerMask playerLayerHit;

        public Gun pistolGun;
        public Gun sniperGun;
        public Gun dualPistolGun;
        
        private void Awake()
        {
            _actorInputMovement = GetComponent<ActorInputMovement>();
            _animator = GetComponent<Animator>();
            _sidescrollerController = GetComponent<SidescrollerController>();
            _sidescrollerController.SetInput(_actorInputMovement);
            _gunGameObjectHandler = GetComponent<EquipingGunController>();
            _attackHandler = GetComponent<AttackHandler>();
            
            _pistolState.SetGun(pistolGun);
            _sniperState.SetGun(sniperGun);
            _dualPistolState.SetGun(dualPistolGun);

            IndividualHealth health = GetComponent<PlayerReceivedDamage>().health;
            health.healthPoint.OnValueChanged += OnHealthChanged;
        }

        private void Start()
        {
            actorGameObject = this.gameObject;
            currentState = _idleState;
            currentState.Enter(this);
        }

        private void OnEnable()
        {
            isControllable = true;
            EventManager.OnPlayerEnteredBossArea.AddListener(OnPlayerEnteredBossArea);
            EventManager.OnReadyForBattle.AddListener(() => isControllable = true);
        }

        private void OnDisable()
        {
            EventManager.OnPlayerEnteredBossArea.RemoveListener(OnPlayerEnteredBossArea);
            EventManager.OnReadyForBattle.RemoveListener(() => isControllable = true);
        }

        private void Update()
        {
            if (IsAllowedToUseController() == false) return;
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
            if (IsAllowedToUseController() == false) return;
            // if (isControllable == false) return;
            bool IsPistolAttackPressed = Input.GetMouseButton(0);
            bool IsSniperAttackPressed = Input.GetMouseButton(1);
            bool IsDualPistolPressed = Input.GetKey(KeyCode.Q);
            float horizontalMovement = Input.GetAxis("Horizontal");
            bool isJumpPressed = Input.GetKey(KeyCode.Space);

            if (IsPistolAttackPressed)
            {
                StopMoving();
                StartAttackWithPistol();
            }else if (IsSniperAttackPressed)
            {
                StopMoving();
                StartAttackWithSniper();
            }else if (IsDualPistolPressed)
            {
                StopMoving();
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
            if (IsCurrentlyInAttackState()) return;
            ChangeState(_pistolState);
        }
        private void StartAttackWithSniper()
        {
            if (IsStateNotAvailableToShoot() == false) return;
            if (IsCurrentlyInAttackState()) return;
            ChangeState(_sniperState);
        }
        
        private void StartAttackWithDualPistol()
        {
            if (IsStateNotAvailableToShoot() == false) return;
            if (IsCurrentlyInAttackState()) return;
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
            _actorInputMovement.ActorMove(horizontal, 0, isJumpPressed);
        }

        private void StopMoving()
        {
            _actorInputMovement.ActorMove(0, 0, false);
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

        private bool IsAllowedToUseController()
        {
            return isActorDied == false && IsHitState == false && isControllable;
        }

        private void OnPlayerEnteredBossArea()
        {
            // StopMoving();
            isControllable = false;
            StopMoving();
            if(IsAirState) _airState.StopAnimation();
            currentState.Exit(this);
            currentState = _idleState;
            currentState.Enter(this);
            
        }


        #region ############################ Animation Event ###################################

        public void AttackAnimationEnd()
        {
            Debug.Log("Attack End");
            if (IsCurrentlyInAttackState() == false) return;
            currentState.Exit(this);
            currentState = _idleState;
            currentState.Enter(this);
        }

        public void FireShot()
        {
            Debug.Log("Attack");
            if (IsSniperState)
            {
                _attackHandler.ShotSniper();
            }else if (IsPistolState || IsDualPistolState)
            {
                _attackHandler.ShotRevolver();
            }
        }
        //          This Animation is for Dual Pistol           //
        public void BothGunFire()
        {
            Debug.Log("BothGunFire");
            _attackHandler.ShotPistol();
            _attackHandler.ShotRevolver();
        }
        
        public void GunOneFire()
        {
            Debug.Log("GunOneFire");
            _attackHandler.ShotPistol();
        }
        
        public void SniperCharge()
        {
            _attackHandler.SniperChargeInit();
        }

        public void RevolverEquip()
        {
            _gunGameObjectHandler.EquipRevolver();
        }

        public void RevolverUnequip()
        {
            _gunGameObjectHandler.UnEquipRevolver();
        }

        public void PistolEquip()
        {
            _gunGameObjectHandler.EquipPistol();
        }

        public void PistolUnequip()
        {
            _gunGameObjectHandler.UnEquipPistol();
        }

        public void SniperEquip()
        {
            _gunGameObjectHandler.EquipSniper();
        }

        public void SniperUnequip()
        {
            _gunGameObjectHandler.UnEquipSniper();
        }

        public void OnHitAnimationEnd()
        {
            Debug.Log("OnHitAnimationEnd");
            currentState.Exit(this);
            currentState = _idleState;
            currentState.Enter(this);
        }

        #endregion

        #region Player Died

        
        public void OnHealthChanged(int health)
        {
            if (health > 0) Hit();
            if (health <= 0)
            {
                Die();
            }
        }
        [Button]
        public void Hit()
        {
            currentState.ChangeState(_hitState);
            if (_hitState.IsOnAirStateHit == false)
            {
                StopMoving();
            }
            _hitState.IsOnAirStateHit = false;
            RevertWeaponToDefaultPosition();
            StartCoroutine(ProcessInvincible());
        }

        IEnumerator ProcessInvincible()
        {
            int player = LayerMask.NameToLayer("Player");
            int playerHit = LayerMask.NameToLayer("PlayerHit");
            SetLayerAllChildrens(gameObject, playerHit);
            
            yield return new WaitForSeconds(durationOfInvincibility);
            SetLayerAllChildrens(gameObject, player);
        }

        private void SetLayerAllChildrens(GameObject _go, int _layer)
        {
            _go.layer = _layer;
            foreach (Transform child in _go.transform)
            {
                child.gameObject.layer = _layer;
 
                Transform _HasChildren = child.GetComponentInChildren<Transform>();
                if (_HasChildren != null)
                    SetLayerAllChildrens(child.gameObject, _layer);
            }
        }

        private void RevertWeaponToDefaultPosition()
        {
            RevolverUnequip();
            SniperUnequip();
            PistolEquip();
        }

        private void Die()
        {
            ChangeState(_dieState);
            Debug.Log("PlayerDied");
        }

        private IEnumerator WaitForSecondsUntilEndScreen()
        {
            yield return new WaitForSeconds(3f);
            EventManager.OnPlayerDied.Invoke();
            Debug.Log("OnPlayerDied");
        }

        #endregion

    }
}