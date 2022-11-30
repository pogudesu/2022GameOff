using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace StageController
{
    public class CameraShake : MonoBehaviour
    {
        public CinemachineVirtualCamera _virtualCamera;
        private Coroutine attackShake;
        private CinemachineBasicMultiChannelPerlin noise;
        private const float NormalFrequency = 40f;
        
        private void Start()
        {
            noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void Shake(float duration, float gain, float frequency = NormalFrequency)
        {
            if(attackShake != null) StopCoroutine(attackShake);
            attackShake = StartCoroutine(AttackShake(duration, gain, frequency));
        }
        
        IEnumerator AttackShake(float duration, float gain, float frequency)
        {
            noise.m_AmplitudeGain = gain;
            noise.m_FrequencyGain = frequency;
            yield return new WaitForSeconds(duration);
            noise.m_AmplitudeGain = 0;
            noise.m_FrequencyGain = NormalFrequency;
        }

        public void ConstantShakeEnable(int gain, float frequency = NormalFrequency)
        {
            noise.m_AmplitudeGain = gain;
            noise.m_FrequencyGain = frequency;
        }
        
        public void ConstantShakeDisable()
        {
            noise.m_AmplitudeGain = 0;
            noise.m_FrequencyGain = NormalFrequency;
        }
        
        
    }
}