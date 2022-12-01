using System;
using System.Collections;
using EventHandler;
using StageController;
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
        public GameObject YouDied;

        private void OnEnable()
        {
            EventManager.OnUnlockedPistol.AddListener(OnFirstWeaponGet);
            EventManager.OnUnlockedSniper.AddListener(OnSecondWeaponGet);
            EventManager.OnUnlockedDualPistol.AddListener(OnThirdWeaponGet);
            EventManager.OnFirstBossDefeat.AddListener(OnNumber5Get);
            EventManager.OnSecondBossDefeat.AddListener(OnNumber3Get);
            EventManager.OnThirdBossDefeat.AddListener(OnNumber7Get);
            EventManager.OnPlayerDiedForUI.AddListener(OnPlayerDiedShowYouDied);
        }

        private void OnDisable()
        {
            EventManager.OnUnlockedPistol.RemoveListener(OnFirstWeaponGet);
            EventManager.OnUnlockedSniper.RemoveListener(OnSecondWeaponGet);
            EventManager.OnUnlockedDualPistol.RemoveListener(OnThirdWeaponGet);
            EventManager.OnFirstBossDefeat.RemoveListener(OnNumber5Get);
            EventManager.OnSecondBossDefeat.RemoveListener(OnNumber3Get);
            EventManager.OnThirdBossDefeat.RemoveListener(OnNumber7Get);
            EventManager.OnPlayerDiedForUI.RemoveListener(OnPlayerDiedShowYouDied);
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
            AnalyticsManager.Instance.FirstBossDefeat();
            Number5.SetActive(true);
        }
        
        private void OnNumber3Get()
        {
            AnalyticsManager.Instance.SecondBossDefeat();
            Number3.SetActive(true);
        }
        
        private void OnNumber7Get()
        {
            AnalyticsManager.Instance.ThirdBossDefeat();
            Number7.SetActive(true);
        }

        private void OnPlayerDiedShowYouDied()
        {
            StartCoroutine(DelayShowYouDied());
        }

        IEnumerator DelayShowYouDied()
        {
            yield return new WaitForSeconds(2f);
            YouDied.SetActive(true);
        }
    }
}