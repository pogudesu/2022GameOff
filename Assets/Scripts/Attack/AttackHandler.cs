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
        [SerializeField] private LayerMask _layerMaskCompanion;
        [SerializeField] private GameObject impactHit;
        [SerializeField] private GameObject sniperImpactHit;
        [SerializeField] private Transform secretTeleportationPosition;
        private bool isTeleported = false;
        private void Start()
        {
            if(!sniperVFX) Debug.LogWarning("Sniper VFX is missing");
            if(!pistolVFX) Debug.LogWarning("Pistol VFX is missing");
            if(!revolverVFX) Debug.LogWarning("Revolver VFX is missing");
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
            if (Physics.Raycast(muzzlePosition, directionShot, out hit, 20f, _layerMaskCompanion))
            {
                Debug.Log("Companion Hit!");
                if (isTeleported == false)
                {
                    isTeleported = true;
                    if (secretTeleportationPosition != null)
                        this.transform.position = secretTeleportationPosition.position;
                }
            }
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(muzzlePosition, directionShot, out hit, 20f, _layerMask))
            {
                
                // Debug.DrawRay(muzzlePosition, directionShot * hit.distance, Color.yellow, 3f);
                Debug.Log("Did Hit");
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    hit.rigidbody.AddForceAtPosition(directionShot * 2000f, hit.point);
                }

                bullet.transform.position = hit.point;
                if (impactHit != null)
                {
                    GameObject impact;
                    if (gunType == GunType.PISTOL || gunType == GunType.DUAL_PISTOL)
                        impact = Instantiate(impactHit, hit.point, transform.rotation);
                    else
                        impact = Instantiate(sniperImpactHit, hit.point, transform.rotation);
                    Destroy(impact, 1.5f);
                }
                IDamageable enemyHit = hit.transform.gameObject.GetComponent<IDamageable>();
                if(enemyHit != null)
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