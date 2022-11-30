using System;
using Audio;
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
                SFXController.PlayHit();
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                damageable.TakeDamage(dashDamage, GunType.SNIPER);
            }
        }

        public void Woosh()
        {
            SFXController.PlayWoosh();
        }
    }
}