using System;
using EventHandler;
using UnityEngine;

namespace Player
{
    public class PlayerEventListener : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        public bool isDevTesting = true;
        private void OnEnable()
        {
            if (isDevTesting)
            {
                _playerData.isSniperUnlocked = false;
                _playerData.isDualPistolUnlocked = false;
            }
            EventManager.OnUnlockedSniper.AddListener(OnUnlockSniperGun);
            EventManager.OnUnlockedDualPistol.AddListener(OnUnlockDualPistolGun);
        }

        private void OnDisable()
        {
            EventManager.OnUnlockedSniper.RemoveListener(OnUnlockSniperGun);
            EventManager.OnUnlockedDualPistol.RemoveListener(OnUnlockDualPistolGun);
        }
        // Note: For testing purpose. Todo, delete this update later
        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Alpha1))
            // {
            //     OnUnlockSniperGun();
            // }
            // if (Input.GetKeyDown(KeyCode.Alpha2))
            // {
            //     OnUnlockDualPistolGun();
            // }
        }

        private void OnUnlockSniperGun()
        {
            if(!_playerData) return;
            _playerData.isSniperUnlocked = true;
        }
        
        private void OnUnlockDualPistolGun()
        {
            if (!_playerData) return;
            _playerData.isDualPistolUnlocked = true;
        }
    }
}