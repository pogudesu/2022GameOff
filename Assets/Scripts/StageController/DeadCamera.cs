using System;
using Cinemachine;
using EventHandler;
using UnityEngine;

namespace StageController
{
    public class DeadCamera : MonoBehaviour
    {
        public CinemachineVirtualCamera camera;

        private void OnEnable()
        {
            EventManager.OnPlayerDiedTriggerFast.AddListener(() => camera.Priority = 30);
        }

        private void OnDisable()
        {
            EventManager.OnPlayerDiedTriggerFast.RemoveListener(() => camera.Priority = 30);
        }
    }
}