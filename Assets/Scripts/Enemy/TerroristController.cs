using System;
using System.Collections;
using Attack;
using Audio;
using EventHandler;
using HealthSystem;
using PlayerGun;
using StateMachine.Data;
using StateMachine.PlayerState;
using UnityEngine;

namespace Enemy
{
    public class TerroristController : ActorData
    {
        private IdleState _idleState = new IdleState();
        private AttackState sniperState = new AttackState();
        private DieState _dieState = new DieState();
        [SerializeField] private Gun sniperGun;
        private EquipingGunController _gunGameObjectHandler;
        private AttackHandler _attackHandler;
        public float intervalBetweenAttack = 2f;
        private bool isPlayerDied = false;

        private void Start()
        {
            _gunGameObjectHandler = GetComponent<EquipingGunController>();
            _attackHandler = GetComponent<AttackHandler>();
            sniperState.SetGun(sniperGun);
            currentState = _idleState;
            nextState = _idleState;
            currentState.Enter(this);

            IndividualHealth health = GetComponent<EnemyReceivedDamage>().health;
            health.healthPoint.OnValueChanged += OnChangedHealthEnemy;
            // Todo later, create event that initiate attack
            // InitAttack();
        }

        private void OnDestroy()
        {
            IndividualHealth health = GetComponent<EnemyReceivedDamage>().health;
            health.healthPoint.OnValueChanged -= OnChangedHealthEnemy;
        }

        private void OnEnable()
        {
            EventManager.OnReadyForBattle.AddListener(InitAttack);
            EventManager.OnPlayerDied.AddListener(() => isPlayerDied = true);

        }

        private void OnDisable()
        {
            EventManager.OnReadyForBattle.RemoveListener(InitAttack);
            EventManager.OnPlayerDied.RemoveListener(() => isPlayerDied = true);
        }

        private void InitAttack()
        {
            if (isPlayerDied) return;
            currentState.ChangeState(sniperState);
        }
        
        public void SniperEquip()
        {
            _gunGameObjectHandler.EquipSniper();
        }

        public void SniperUnequip()
        {
            _gunGameObjectHandler.UnEquipSniper();
        }
        
        public void FireShot()
        {
            Debug.Log("Attack");
            _attackHandler.ShotSniper();
            EventManager.CameraShakeHigh.Invoke();
            SFXController.PlaySniper();
        }
        
        public void AttackAnimationEnd()
        {
            Debug.Log("Attack End");
            currentState.Exit(this);
            currentState = _idleState;
            currentState.Enter(this);
            StartCoroutine(WaitIntervalBetweenAttack());
        }

        IEnumerator WaitIntervalBetweenAttack()
        {
            yield return new WaitForSeconds(intervalBetweenAttack);
            InitAttack();
        }
        
        public void SniperCharge()
        {
            _attackHandler.SniperChargeInit();
            SFXController.PlayCharging();
        }
        public void PistolEquip(){}
        public void PistolUnequip(){}

        #region Event

        public void OnChangedHealthEnemy(int health)
        {
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            currentState.ChangeState(_dieState);
            EventManager.OnBossDeath.Invoke();
            StartCoroutine(WaitUntilShowNumber());
        }

        IEnumerator WaitUntilShowNumber()
        {
            yield return new WaitForSeconds(2f);
            EventManager.OnFirstBossDefeat.Invoke();
        }

        #endregion
    }
}