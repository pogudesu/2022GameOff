using System;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace HealthSystem
{
    [Serializable]
    public class IndividualHealth
    {
        public IntVariable healthPoint = null;
        public IntVariable maxHealthPoint = null;
    
        public IndividualHealth(IntVariable health, IntVariable maxHealth)
        {
            this.healthPoint = health;
            this.maxHealthPoint = maxHealth;
            Initialize();
        }

        public void Initialize()
        {
            ChangeHealthValue(healthPoint.Value);
        }


        public void TakeDamage(int damage)
        {
            int newHealth = healthPoint.Value - damage;
            ChangeHealthValue(newHealth);
            // if (healthPoint.Value < 0) healthPoint.Value = 0;
        }

        public void IncrementMaxHealthPoint(int additionalHealth)
        {
            float mathSignNumber = Mathf.Sign((float)additionalHealth);
            if (mathSignNumber < 0) return;
            maxHealthPoint.Value += additionalHealth;
        }

        public void RecoverHealthPoint(int amount)
        {
            int newHealth = healthPoint.Value + amount;
            ChangeHealthValue(newHealth);
        }

        public void ChangeHealthValue(int Value)
        {
            if (healthPoint.Value <= 0) return;
            healthPoint.Value = Math.Clamp(Value, 0, maxHealthPoint.Value);
        }
    
    }
}