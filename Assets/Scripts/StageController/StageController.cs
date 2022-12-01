using System;
using System.Collections;
using System.Collections.Generic;
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
        public bool isOnCutScene = false;
        private List<CameraShake> _cameraShakes = new List<CameraShake>();

        private void Awake()
        {
            CameraShake[] cameras = GetComponents<CameraShake>();
            foreach (var cam in cameras)
            {
                _cameraShakes.Add(cam);
            }
            // _cameraShake = GetComponent<CameraShake>();
        }

        public void ShakeCameraLow()
        {
            foreach (var _cameraShake in _cameraShakes)
            {
                _cameraShake.Shake(0.1f, 0.2f);
            }
        }

        public void ShakeCameraHigh()
        {
            foreach (var _cameraShake in _cameraShakes)
            {
                _cameraShake.Shake(0.24f, 1f);
            }
        }

        private void OnEnable()
        {
            EventManager.OnPlayerEnteredBossArea.AddListener(OnPlayerEnteredBossArea);
            EventManager.OnPlayerDied.AddListener(OnPlayerDied);
            EventManager.CameraShakeLow.AddListener(ShakeCameraLow);
            EventManager.CameraShakeHigh.AddListener(ShakeCameraHigh);
        }

        private void OnPlayerDied()
        {
            if (isOnCutScene == true) return;
            EventManager.OnPlayerDiedForUI.Invoke();
            AnalyticsManager.Instance.PlayerDied();
        }

        private void OnDisable()
        {
            EventManager.OnPlayerEnteredBossArea.RemoveListener(OnPlayerEnteredBossArea);
            EventManager.OnPlayerDied.RemoveListener(OnPlayerDied);
            EventManager.CameraShakeLow.RemoveListener(ShakeCameraLow);
            EventManager.CameraShakeHigh.RemoveListener(ShakeCameraHigh);
        }

        private void OnPlayerEnteredBossArea()
        {
            isInBossBattle = true;
            RaiseUpInvisibleWall();
            DisableTrigger();
            ChangeCameraToBossCamera();
            EventManager.OnShowEnemyHealth.Invoke();
            EventManager.OnShowPlayerHealth.Invoke();
            // StartCoroutine(WaitForSecondsToReadyForBattle());
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