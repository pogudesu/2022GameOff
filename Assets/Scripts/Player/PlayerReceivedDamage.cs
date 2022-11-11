using Damageable;
using HealthSystem;
using PlayerGun;
using UnityEngine;

namespace Player
{
    public class PlayerReceivedDamage : MonoBehaviour, IDamageable
    {

        public IndividualHealth health;
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