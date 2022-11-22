using System;
using Damageable;
using HealthSystem;
using PlayerGun;
using UnityEngine;

namespace Player
{
    public class PlayerReceivedDamage : MonoBehaviour, IDamageable
    {

        public IndividualHealth health;

        // private void Start()
        // {
        //     health.healthPoint.Value = health.maxHealthPoint.Value;
        // }

        public void TakeDamage(int attackDamage, GunType attackType)
        {
            health.TakeDamage(attackDamage);
            Hit();
        }

        private void Hit()
        {
            // throw new System.NotImplementedException();
        }
    }
}