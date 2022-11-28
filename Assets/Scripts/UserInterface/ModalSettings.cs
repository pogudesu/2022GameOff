using System;
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
            throw new NotImplementedException();
        }

        private void OnDisable()
        {
            throw new NotImplementedException();
        }

        private void OnFirstWeaponGet()
        {
            firstWeaponGet.SetActive(true);
        }
        
        private void OnSecondWeaponGet()
        {
            secondWeaponGet.SetActive(true);
        }
        
        private void OnThirdWeaponGet()
        {
            thirdWeaponGet.SetActive(true);
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