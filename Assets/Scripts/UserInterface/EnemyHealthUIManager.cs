using System;
using EventHandler;
using UnityEngine;

namespace UserInterface
{
    public class EnemyHealthUIManager : MonoBehaviour
    {
        public GameObject parent;
        private void OnEnable()
        {
            EventManager.OnShowEnemyHealth.AddListener(OnShowUI);
            EventManager.OnBossDeath.AddListener(OnHideUI);
        }

        private void OnDisable()
        {
            EventManager.OnShowEnemyHealth.RemoveListener(OnShowUI);
            EventManager.OnBossDeath.RemoveListener(OnHideUI);
        }

        private void OnShowUI()
        {
            parent.SetActive(true);
        }
        private void OnHideUI()
        {
            parent.SetActive(false);
        }
    }
}