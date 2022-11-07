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
            if(!revolverEquipedGameObject) Debug.LogError("Revolver Equip is missing!");
            if(!revolverUnequipedGameObject) Debug.LogError("Revolver Unequip is missing!");
            if(!pistolEquipedGameObject) Debug.LogError("Pistol Equip is missing!");
            if(!pistolUnequipedGameObject) Debug.LogError("Pistol Unequip is missing!");
            if(!sniperEquipedGameObject) Debug.LogError("Sniper Equip is missing!");
            if(!sniperUnEquipedGameObject) Debug.LogError("Sniper Unequip is missing!");
        }

        public void EquipRevolver()
        {
            revolverUnequipedGameObject.SetActive(false);
            revolverEquipedGameObject.SetActive(true);
        }

        public void UnEquipRevolver()
        {
            revolverEquipedGameObject.SetActive(false);
            revolverUnequipedGameObject.SetActive(true);
        }

        public void EquipPistol()
        {
            pistolUnequipedGameObject.SetActive(false);
            pistolEquipedGameObject.SetActive(true);
        }

        public void UnEquipPistol()
        {
            pistolEquipedGameObject.SetActive(false);
            pistolUnequipedGameObject.SetActive(true);
        }

        public void EquipSniper()
        {
            sniperUnEquipedGameObject.SetActive(false);
            sniperEquipedGameObject.SetActive(true);
        }

        public void UnEquipSniper()
        {
            sniperEquipedGameObject.SetActive(false);
            sniperUnEquipedGameObject.SetActive(true);
        }
    }
}