using System;
using System.Collections.Generic;
using EventHandler;
using UnityEngine;

namespace Attack
{
    public class GroundPoundAttackParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem groundPoundParticle;
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
            groundPoundParticle.Play();
        }
    }
}