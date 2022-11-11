using System;
using Damageable;
using PlayerGun;
using UnityEngine;

namespace Attack
{
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField]private ParticleSystem sniperVFX;
        [SerializeField]private ParticleSystem sniperChargeVFX;
        [SerializeField]private ParticleSystem pistolVFX;
        [SerializeField]private ParticleSystem revolverVFX;
        [SerializeField] private Transform shotForwardTransform;
        [SerializeField] private TrailRenderer trailBullet;
        // [SerializeField] private LineRenderer lineBullet;
        [SerializeField] private LayerMask _layerMask;
        private void Start()
        {
            if(!sniperVFX) Debug.LogError("Sniper VFX is missing");
            if(!pistolVFX) Debug.LogError("Pistol VFX is missing");
            if(!revolverVFX) Debug.LogError("Revolver VFX is missing");
        }

        public void ShotSniper()
        {
            StopSniperCharge();
            sniperVFX.Play();
            ShotRaycast(sniperVFX.transform, GunType.SNIPER);
        }

        public void SniperChargeInit()
        {
            sniperChargeVFX.Play();
        }

        public void ShotPistol()
        {
            pistolVFX.Play();
            ShotRaycast(pistolVFX.transform, GunType.DUAL_PISTOL);
        }

        public void ShotRevolver()
        {
            revolverVFX.Play();
            ShotRaycast(revolverVFX.transform, GunType.PISTOL);
        }

        public void ShotRaycast(Transform gunTransformMuzzle, GunType gunType)
        {
            StopSniperCharge();
            // Bit shift the index of the layer (8) to get a bit mask
            Vector3 muzzlePosition = gunTransformMuzzle.position;
            Vector3 directionShot = shotForwardTransform.TransformDirection(Vector3.forward);
            
            var bullet = Instantiate(trailBullet, muzzlePosition, Quaternion.identity);
            bullet.AddPosition(muzzlePosition);
            
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(muzzlePosition, directionShot, out hit, 20f, _layerMask))
            {
                
                // Debug.DrawRay(muzzlePosition, directionShot * hit.distance, Color.yellow, 3f);
                Debug.Log("Did Hit");

                bullet.transform.position = hit.point;
                IDamageable enemyHit = hit.transform.gameObject.GetComponent<IDamageable>();
                enemyHit.TakeDamage(15, gunType);
            }
            else
            {
                // Debug.DrawRay(muzzlePosition, directionShot * 1000, Color.blue, 3f);
                Debug.Log("Did not Hit");
                // bullet.transform.position = directionShot * 20f;
                bullet.transform.position = muzzlePosition + (directionShot * 20f);
            }
        }

        private void StopSniperCharge()
        {

            sniperChargeVFX.Stop();
            
        }
    }
}