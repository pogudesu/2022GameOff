using System;
using System.Collections;
using EventHandler;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StageController
{
    public class SlowMo : MonoBehaviour
    {
        private Coroutine slowMoRoutine = null;
        public float slowTime = 0.1f;
        public float slowTimeDuration = 0.1f;
        private void OnEnable()
        {
            EventManager.SlowMo.AddListener(SlowMotion);
        }

        private void OnDisable()
        {
            EventManager.SlowMo.RemoveListener(SlowMotion);
        }

        private void SlowMotion()
        {
            Time.timeScale = slowTime;
            if (slowMoRoutine != null)
            {
                slowMoRoutine = null;
            }

            slowMoRoutine = StartCoroutine(SlowMotionDelay());
        }

        IEnumerator SlowMotionDelay()
        {
            yield return new WaitForSeconds(slowTimeDuration);
            Time.timeScale = 1f;
        }
    }
}