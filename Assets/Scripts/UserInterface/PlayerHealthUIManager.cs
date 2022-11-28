using System;
using EventHandler;
using UnityEngine;

namespace UserInterface
{
    public class PlayerHealthUIManager : MonoBehaviour
    {

        public GameObject parent;
        private void OnEnable()
        {
            EventManager.OnShowPlayerHealth.AddListener(OnShowUI);
            EventManager.OnBossDeath.AddListener(OnHideUI);
        }

        private void OnDisable()
        {
            EventManager.OnShowPlayerHealth.RemoveListener(OnShowUI);
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