using System;
using Obvious.Soap;
using UnityEngine;

namespace Player
{
    public class PlayerFallTrigger : MonoBehaviour
    {
        public IntVariable playerHealth;

        private void OnTriggerEnter(Collider other)
        {
            playerHealth.Value = 0;
        }
    }
}