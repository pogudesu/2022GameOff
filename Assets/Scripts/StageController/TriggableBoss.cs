using System;
using EventHandler;
using UnityEngine;

namespace StageController
{
    public class TriggableBoss : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.OnPlayerEnteredBossArea.Invoke();
            }
        }
    }
}