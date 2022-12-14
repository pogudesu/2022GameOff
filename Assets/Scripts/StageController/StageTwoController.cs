using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
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
        

        private void Start()
        {

        }

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
            EventManager.CameraShakeHigh.Invoke();
            StartCoroutine(DelayGroundPoundDamage());
        }

        IEnumerator DelayGroundPoundDamage()
        {
            yield return new WaitForSeconds(0.15f);
            EventManager.CameraShakeHigh.Invoke();
            groundPoundCollosion.enabled = true;
            yield return new WaitForSeconds(0.2f);
            groundPoundCollosion.enabled = false;
        } 

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                SFXController.PlayHit();
                IDamageable iDamage = collision.gameObject.GetComponent<IDamageable>();
                iDamage.TakeDamage(groundPoundDamage, GunType.SNIPER);
            }
        }
    }
}