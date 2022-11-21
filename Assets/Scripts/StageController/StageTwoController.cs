using System;
using System.Collections;
using System.Collections.Generic;
using Damageable;
using EventHandler;
using Player;
using PlayerGun;
using UnityEngine;

namespace StageController
{
    public class StageTwoController : MonoBehaviour
    {
        [SerializeField] private BoxCollider groundPoundCollosion;
        public int groundPoundDamage = 10;
        private void OnEnable()
        {
            EventManager.OnGroundPound.AddListener(OnGroundPound);
        }

        private void OnDisable()
        {
            EventManager.OnGroundPound.RemoveListener(OnGroundPound);
        }

        private void OnGroundPound()
        {
            StartCoroutine(DelayGroundPoundDamage());
        }

        IEnumerator DelayGroundPoundDamage()
        {
            yield return new WaitForSeconds(0.15f);
            groundPoundCollosion.enabled = true;
            yield return new WaitForSeconds(0.2f);
            groundPoundCollosion.enabled = false;
        } 

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                IDamageable iDamage = collision.gameObject.GetComponent<IDamageable>();
                iDamage.TakeDamage(groundPoundDamage, GunType.SNIPER);
            }
        }
    }
}