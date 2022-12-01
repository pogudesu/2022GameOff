using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Enemy.AttackComponent;
using Enemy.State;
using EventHandler;
using HealthSystem;
using State.Interface;
using StateMachine.Data;
using StateMachine.PlayerState;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class GolemController : ActorData
    {
        private IdleState _idleState = new IdleState();
        private GroundPound _groundPound = new GroundPound();
        private Projectile _projectile = new Projectile();
        private DieState _dieState = new DieState();
        private List<int> randomAttack = new List<int>();
        [SerializeField] private Transform playerTransform;
        private int projectileStartingWeight = 7;
        private int groundPoundStartingWeight = 3;
        private int totalNumOfSkill = 2;
        public float mindurationForEachAttack = 1f;
        public float maxdurationForEachAttack = 1f;
        private ProjectileHandler _projectileHandler;
        private bool isPlayerDied = false;
        private void Start()
        {
            IndividualHealth health = GetComponent<EnemyReceivedDamage>().health;
            health.healthPoint.OnValueChanged += OnChangedHealthEnemy;

            _projectileHandler = GetComponent<ProjectileHandler>();
            
            currentState = _idleState;
            currentState.Enter(this);
            
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
            randomAttack.Clear();
            randomAttack.Add(projectileStartingWeight); // index 0
            randomAttack.Add(groundPoundStartingWeight); // index 1
            PickAttack();            
        }


        private void PickAttack()
        {
            IStateable selectedAttackState = RandomizeAttack();
            
            currentState.ChangeState(selectedAttackState);
        }

        private IStateable RandomizeAttack()
        {
            if (randomAttack.Count != 2) return _idleState;
            int totalAttackWeight = randomAttack[0] + randomAttack[1];
            int randomNumberAttackType = Random.Range(1, totalAttackWeight + 1);
            if (randomAttack[0] <= randomNumberAttackType)
            {
                return _groundPound;
            }
            return _projectile;
        }

        #region Animation Event


        public void EndOfAnimation()
        {
            Debug.Log("Hello World");
            currentState.ChangeState(_idleState);
            StartCoroutine(WaitIntervalUntilNextAttack());
        }

        public void GroundPoundAttackEvent()
        {
            SFXController.PlayGroundPound();
            EventManager.OnGroundPound.Invoke();
        }

        public void Cast()
        {
            SFXController.PlayCast();
            _projectileHandler.AttackTowards(playerTransform);
        }
        
        
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
            EventManager.OnSecondBossDefeat.Invoke();
        }

        private IEnumerator WaitIntervalUntilNextAttack()
        {
            float randomDuration = Random.Range(mindurationForEachAttack, maxdurationForEachAttack + 1);
            yield return new WaitForSeconds(randomDuration);
            InitAttack();
        }

        #endregion
    }
    
}