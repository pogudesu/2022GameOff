using System;
using EventHandler;
using UnityEngine;

namespace UserInterface
{
    public class ModalSettings : MonoBehaviour
    {
        public GameObject firstWeaponGet;
        public GameObject secondWeaponGet;
        public GameObject thirdWeaponGet;
        public GameObject Number5;
        public GameObject Number3;
        public GameObject Number7;

        private void OnEnable()
        {
            EventManager.OnUnlockedPistol.AddListener(OnFirstWeaponGet);
            EventManager.OnUnlockedSniper.AddListener(OnSecondWeaponGet);
            EventManager.OnUnlockedDualPistol.AddListener(OnThirdWeaponGet);
            EventManager.OnFirstBossDefeat.AddListener(OnNumber5Get);
            EventManager.OnSecondBossDefeat.AddListener(OnNumber3Get);
            EventManager.OnThirdBossDefeat.AddListener(OnNumber7Get);
        }

        private void OnDisable()
        {
            EventManager.OnUnlockedPistol.RemoveListener(OnFirstWeaponGet);
            EventManager.OnUnlockedSniper.RemoveListener(OnSecondWeaponGet);
            EventManager.OnUnlockedDualPistol.RemoveListener(OnThirdWeaponGet);
            EventManager.OnFirstBossDefeat.RemoveListener(OnNumber5Get);
            EventManager.OnSecondBossDefeat.RemoveListener(OnNumber3Get);
            EventManager.OnThirdBossDefeat.RemoveListener(OnNumber7Get);
        }

        private void OnFirstWeaponGet()
        {
            Cursor.visible = true;
            firstWeaponGet.SetActive(true);
        }
        
        private void OnSecondWeaponGet()
        {
            Cursor.visible = true;
            secondWeaponGet.SetActive(true);
        }
        
        private void OnThirdWeaponGet()
        {
            Cursor.visible = true;
            thirdWeaponGet.SetActive(true);
        }

        public void CloseModal()
        {
            Cursor.visible = false;
        }
        
        private void OnNumber5Get()
        {
            Number5.SetActive(true);
        }
        
        private void OnNumber3Get()
        {
            Number3.SetActive(true);
        }
        
        private void OnNumber7Get()
        {
            Number7.SetActive(true);
        }
    }
}