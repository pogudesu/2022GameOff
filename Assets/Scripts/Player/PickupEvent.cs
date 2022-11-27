using System;
using System.Security.Cryptography;
using EventHandler;
using PlayerGun;
using UnityEngine;

namespace Player
{
    public class PickupEvent : MonoBehaviour
    {
        public GunType type;
        [SerializeField] private PlayerData _playerData;

        private void Start()
        {
            if (_playerData == null) return;
            if(type == GunType.SNIPER && _playerData.isSniperUnlocked) Destroy(this.gameObject);
            if(type == GunType.DUAL_PISTOL && _playerData.isDualPistolUnlocked) Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                switch (type)
                {
                    case GunType.SNIPER:
                        EventManager.OnUnlockedSniper.Invoke();
                        _playerData.currentStage += 1;
                        _playerData.isSniperUnlocked = true;
                        Destroy(this.gameObject);
                        break;
                    case GunType.DUAL_PISTOL:
                        EventManager.OnUnlockedDualPistol.Invoke();
                        _playerData.currentStage += 1;
                        _playerData.isDualPistolUnlocked = true;
                        _playerData.isSniperUnlocked = true;
                        Destroy(this.gameObject);
                        break;
                }
            }
        }
    }
}