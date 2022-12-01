using System;
using UnityEngine;

namespace Enemy
{
    public class FinalBossInitiator : MonoBehaviour
    {
        [SerializeField] private GameObject model;
        private void SetActive()
        {
            model.SetActive(true);
        }
    }
}