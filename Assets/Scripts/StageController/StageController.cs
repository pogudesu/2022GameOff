using System;
using System.Collections;
using Cinemachine;
using EventHandler;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StageController
{
    public class StageController : MonoBehaviour
    {
        public GameObject BeforeBossWall;
        public GameObject TriggerBossGameObject;
        public CinemachineVirtualCamera playerCamera; 
        public CinemachineVirtualCamera bossCamera;
        public bool isInBossBattle;
        public float durationForReady =2f;
        
        private void OnEnable()
        {
            EventManager.OnPlayerEnteredBossArea.AddListener(OnPlayerEnteredBossArea);
            EventManager.OnPlayerDied.AddListener(OnPlayerDied);
        }

        private void OnPlayerDied()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDisable()
        {
            EventManager.OnPlayerEnteredBossArea.RemoveListener(OnPlayerEnteredBossArea);
            EventManager.OnPlayerDied.RemoveListener(OnPlayerDied);
        }

        private void OnPlayerEnteredBossArea()
        {
            isInBossBattle = true;
            RaiseUpInvisibleWall();
            DisableTrigger();
            ChangeCameraToBossCamera();
            StartCoroutine(WaitForSecondsToReadyForBattle());
        }

        IEnumerator WaitForSecondsToReadyForBattle()
        {
            yield return new WaitForSeconds(durationForReady);
            EventManager.OnReadyForBattle.Invoke();
        }
        private void ChangeCameraToBossCamera()
        {
            bossCamera.Priority = 30;
        }

        private void DisableTrigger()
        {
            if (TriggerBossGameObject != null)
                TriggerBossGameObject.SetActive(false);
        }

        private void RaiseUpInvisibleWall()
        {
            if (BeforeBossWall != null)
                BeforeBossWall.SetActive(true);
        }
    }
}