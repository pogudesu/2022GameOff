using System;
using System.Collections;
using Attack;
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

        private void Start()
        {
            _gunGameObjectHandler = GetComponent<EquipingGunController>();
            _attackHandler = GetComponent<AttackHandler>();
            sniperState.SetGun(sniperGun);
            currentState = _idleState;
            nextState = _idleState;
            currentState.Enter(this);

            IndividualHealth health = GetComponent<EnemyReceivedDamage>().health;
            health.healthPoint.OnValueChanged += OnChangedHealth;
            // Todo later, create event that initiate attack
            // InitAttack();
        }

        private void OnEnable()
        {
            EventManager.OnReadyForBattle.AddListener(InitAttack);
        }

        private void OnDisable()
        {
            EventManager.OnReadyForBattle.RemoveListener(InitAttack);
        }

        private void InitAttack()
        {
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
        }
        public void PistolEquip(){}
        public void PistolUnequip(){}

        #region Event

        public void OnChangedHealth(int health)
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