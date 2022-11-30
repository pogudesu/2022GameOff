using System;
using EventHandler;
using UnityEngine;

namespace Audio
{
    public class AudioBGMController : MonoBehaviour
    {
        [SerializeField] private AudioClip mainBgm;
        [SerializeField] private AudioClip bossBgm;
        [SerializeField] private AudioClip outro;
        private AudioSource _audioSource;


        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            if(mainBgm)
                PlayAudio(mainBgm);
        }

        private void OnEnable()
        {
            EventManager.OnReadyForBattle.AddListener(OnBattle);
            EventManager.OnPlayerEnteredBossArea.AddListener(StopAudio);
        }

        private void OnDisable()
        {
            EventManager.OnReadyForBattle.RemoveListener(OnBattle);
            EventManager.OnPlayerEnteredBossArea.RemoveListener(StopAudio);
        }

        private void StopAudio()
        {
            _audioSource.Stop();
        }

        private void PlayAudio(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
            _audioSource.loop = true;
        }

        private void OnBattle()
        {
            PlayAudio(bossBgm);
        }

        private void OnEnteredBoss()
        {
            
        }

        public void PlayOutro()
        {
            StopAudio();
            if (outro)
            {
                PlayAudio(outro);
            }
        }
    }
}