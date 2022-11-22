
using System.Collections;
using HealthSystem;
using Obvious.Soap;
using UnityEngine;
using Damageable;
using PlayerGun;

namespace Enemy
{
    public class EnemyReceivedDamage : MonoBehaviour, IDamageable
    {
        public IndividualHealth health;
        public IntVariable armor;
        public FloatVariable shield;
        [Range(0,1)]
        public float pistolDamageReduction;
        [Range(0,1)]
        public float sniperDamageReduction;
        [Range(0,1)]
        public float revolverDamageReduction;
        private bool isShieldBroken = false;
        private Coroutine hitCoroutine = null;
        private void Start()
        {
            health.Initialize();
        }

        public virtual void TakeDamage(int attackDamage, GunType damageType)
        {
            float newDamageAfterAttackTypeReduction = GetDamageAfterReduction(attackDamage, damageType);
            if (isShieldBroken == false && CalculateShieldReduction(newDamageAfterAttackTypeReduction))
            {
                return;
            }

            int newDamageAfterArmorReduction = (int)newDamageAfterAttackTypeReduction - armor.Value;
            if (newDamageAfterArmorReduction <= 0) return;
            health.TakeDamage(newDamageAfterArmorReduction);
            Hit();
        }

        private void Hit()
        {
            if (hitCoroutine != null)
            {
                StopCoroutine(hitCoroutine);
            }

            hitCoroutine = StartCoroutine(ProcessHighlights());
        }
        
        IEnumerator ProcessHighlights()
        {
            int enemy = LayerMask.NameToLayer("Enemy");
            int enemyHit = LayerMask.NameToLayer("EnemyHit");
            SetLayerAllChildrens(gameObject, enemyHit);
            
            yield return new WaitForSeconds(0.1f);
            SetLayerAllChildrens(gameObject, enemy);
            hitCoroutine = null;
        }
        
        private void SetLayerAllChildrens(GameObject _go, int _layer)
        {
            _go.layer = _layer;
            foreach (Transform child in _go.transform)
            {
                child.gameObject.layer = _layer;
 
                Transform _HasChildren = child.GetComponentInChildren<Transform>();
                if (_HasChildren != null)
                    SetLayerAllChildrens(child.gameObject, _layer);
            }
        }

        private bool CalculateShieldReduction(float newDamageAfterAttackTypeReduction)
        {
            shield.Value -= newDamageAfterAttackTypeReduction;
            if (shield.Value <= 0)
            {
                isShieldBroken = true;
                return false;
            }
            return true;
        }

        private float GetDamageAfterReduction(int damage, GunType damageType)
        {
            switch (damageType)
            {
                case GunType.PISTOL:
                    return CalculateDamageReduction(damage, pistolDamageReduction);
                case GunType.SNIPER:
                    return CalculateDamageReduction(damage, sniperDamageReduction);
                case GunType.DUAL_PISTOL:
                    return CalculateDamageReduction(damage, revolverDamageReduction);
            }

            return damage;
        }

        private float CalculateDamageReduction(float damage, float reductionPercent)
        {
            float percent = 1f - reductionPercent;
            return damage - percent;
        }
    }
}