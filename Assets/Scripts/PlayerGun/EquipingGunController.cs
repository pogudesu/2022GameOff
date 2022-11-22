using System;
using UnityEngine;

namespace PlayerGun
{
    public class EquipingGunController : MonoBehaviour
    {
        [SerializeField] private GameObject revolverEquipedGameObject;
        [SerializeField] private GameObject revolverUnequipedGameObject;

        [SerializeField] private GameObject pistolEquipedGameObject;
        [SerializeField] private GameObject pistolUnequipedGameObject;

        [SerializeField] private GameObject sniperEquipedGameObject;
        [SerializeField] private GameObject sniperUnEquipedGameObject;

        private void Start()
        {
            if(!revolverEquipedGameObject) Debug.LogWarning("Revolver Equip is missing!");
            if(!revolverUnequipedGameObject) Debug.LogWarning("Revolver Unequip is missing!");
            if(!pistolEquipedGameObject) Debug.LogWarning("Pistol Equip is missing!");
            if(!pistolUnequipedGameObject) Debug.LogWarning("Pistol Unequip is missing!");
            if(!sniperEquipedGameObject) Debug.LogWarning("Sniper Equip is missing!");
            if(!sniperUnEquipedGameObject) Debug.LogWarning("Sniper Unequip is missing!");
        }

        public void EquipRevolver()
        {
            if (IsThereNullGunGameObject()) return;
            revolverUnequipedGameObject.SetActive(false);
            revolverEquipedGameObject.SetActive(true);
        }

        public void UnEquipRevolver()
        {
            if (IsThereNullGunGameObject()) return;
            revolverEquipedGameObject.SetActive(false);
            revolverUnequipedGameObject.SetActive(true);
        }

        public void EquipPistol()
        {
            if (IsThereNullGunGameObject()) return;
            pistolUnequipedGameObject.SetActive(false);
            pistolEquipedGameObject.SetActive(true);
        }

        public void UnEquipPistol()
        {
            if (IsThereNullGunGameObject()) return;
            pistolEquipedGameObject.SetActive(false);
            pistolUnequipedGameObject.SetActive(true);
        }

        public void EquipSniper()
        {
            if (IsThereNullGunGameObject()) return;
            sniperUnEquipedGameObject.SetActive(false);
            sniperEquipedGameObject.SetActive(true);
        }

        public void UnEquipSniper()
        {
            if (IsThereNullGunGameObject()) return;
            sniperEquipedGameObject.SetActive(false);
            sniperUnEquipedGameObject.SetActive(true);
        }

        private bool IsThereNullGunGameObject()
        {
            if (revolverUnequipedGameObject == null) return true;
            if (revolverEquipedGameObject == null) return true;
            if (pistolEquipedGameObject == null) return true;
            if (pistolUnequipedGameObject == null) return true;
            if (sniperEquipedGameObject == null) return true;
            if (sniperUnEquipedGameObject == null) return true;
            return false;
        }
    }
}