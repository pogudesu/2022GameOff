using System.Collections;
using System.Collections.Generic;
using Enemy.AttackComponent;
using Enemy.State;
using EventHandler;
using HealthSystem;
using State.Interface;
using StateMachine.Data;
using StateMachine.PlayerState;
using UnityEngine;

namespace Enemy
{
    public class DragonController : ActorData
    {
        private IdleState _idleState = new IdleState();
        private GroundPound _groundPound = new GroundPound();
        private Projectile _projectile = new Projectile();
        private ProjectileAir _projectileAir = new ProjectileAir();
        private DieState _dieState = new DieState();
        private Dictionary<DragonAttack, int> dragonAttack = new Dictionary<DragonAttack, int>();
        [SerializeField] private Transform playerTransform;
        private int projectileStartingLandWeight = 5;
        private int projectileStartingAirWeight = 5;
        private int groundPoundStartingWeight = 3;
        private int totalNumOfSkill = 3;
        public float mindurationForEachAttack = 1f;
        public float maxdurationForEachAttack = 1f;
        [SerializeField] private ProjectileHandler _projectileLandHandler;
        [SerializeField] private ProjectileHandler _projectileAirHandler;
        private void Start()
        {
            IndividualHealth health = GetComponent<EnemyReceivedDamage>().health;
            health.healthPoint.OnValueChanged += OnChangedHealth;


            
            currentState = _idleState;
            currentState.Enter(this);

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
            dragonAttack[DragonAttack.AirProjectile] = projectileStartingAirWeight; 
            dragonAttack[DragonAttack.LandProjectile] = projectileStartingLandWeight;
            dragonAttack[DragonAttack.GroundPound] = groundPoundStartingWeight; 
            PickAttack();            
        }


        private void PickAttack()
        {
            IStateable selectedAttackState = RandomizeAttack();
            
            currentState.ChangeState(selectedAttackState);
        }

        private IStateable RandomizeAttack()
        {
            if (dragonAttack.Count != 3) return _idleState;
            int totalAttackWeight = dragonAttack[DragonAttack.AirProjectile] + dragonAttack[DragonAttack.LandProjectile] + dragonAttack[DragonAttack.GroundPound];
            int randomNumberAttackType = Random.Range(1, totalAttackWeight + 1);
            foreach (KeyValuePair<DragonAttack, int> attack in dragonAttack)
            {
                if (randomNumberAttackType <= attack.Value)
                {
                    switch (attack.Key)
                    {
                        case DragonAttack.AirProjectile:
                            return _projectileAir;
                        case DragonAttack.LandProjectile:
                            return _projectile;
                        case DragonAttack.GroundPound:
                            return _groundPound;
                        default:
                            return _idleState;
                    }
                }
                else
                {
                    randomNumberAttackType -= attack.Value;
                }
            }
            return _projectile;
        }

        #region Animation Event


        public void EndOfAnimation()
        {
            currentState.ChangeState(_idleState);
            StartCoroutine(WaitIntervalUntilNextAttack());
        }

        public void GroundPoundAttackEvent()
        {
            EventManager.OnGroundPound.Invoke();
        }

        public void Cast()
        {
            _projectileLandHandler.AttackTowards(playerTransform);
        }
        
        public void CastWhileInAir()
        {
            _projectileAirHandler.AttackTowards(playerTransform);
        }
        
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
        }

        private IEnumerator WaitIntervalUntilNextAttack()
        {
            float randomDuration = Random.Range(mindurationForEachAttack, maxdurationForEachAttack + 1);
            yield return new WaitForSeconds(randomDuration);
            InitAttack();
        }

        #endregion
    }


    public enum DragonAttack
    {
        LandProjectile,
        AirProjectile,
        GroundPound
    }
}