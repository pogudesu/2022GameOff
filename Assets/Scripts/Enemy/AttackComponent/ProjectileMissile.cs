using System;
using Audio;
using Damageable;
using PlayerGun;
using UnityEngine;

namespace Enemy.AttackComponent
{
    public class ProjectileMissile : MonoBehaviour
    {
        private Transform playerTransform;
        public int damage = 20;
        public void Init(float force, Transform player)
        {
            playerTransform = player;
            Vector3 direction = player.position - this.transform.position;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody)
            {
                rigidbody.AddForce(direction * force);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                SFXController.PlayHit();
                IDamageable iDamage = other.gameObject.GetComponent<IDamageable>();
                iDamage.TakeDamage(damage, GunType.SNIPER);
            }
        }
    }
}