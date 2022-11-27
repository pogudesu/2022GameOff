using System;
using Damageable;
using PlayerGun;
using UnityEngine;

namespace Enemy
{
    public class CompanionBossController : DragonController
    {

        public int dashDamage = 10;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                damageable.TakeDamage(dashDamage, GunType.SNIPER);
            }
        }
    }
}