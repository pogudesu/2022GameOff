using System;
using UnityEngine;

namespace Audio
{
    public class SFXController : MonoBehaviour
    {
        private static SFXController Instance;
        [SerializeField] private AudioClip leftFoot;
        [SerializeField] private AudioClip rightFoot;
        [SerializeField] private AudioClip jump;
        [SerializeField] private AudioClip landing;
        [SerializeField] private AudioClip pistol;
        [SerializeField] private AudioClip revolver;
        [SerializeField] private AudioClip sniper;
        [SerializeField] private AudioClip charging;
        [SerializeField] private AudioClip hit;
        [SerializeField] private AudioClip cast;
        [SerializeField] private AudioClip groundPound;
        [SerializeField] private AudioClip woosh;
        [SerializeField] private AudioClip uiOpen;
        
        
        
        [SerializeField]private AudioSource _audioSource;
        [SerializeField]private AudioSource _audioSourceJump;

        private void Start()
        {
            Instance = this;
        }

        public static void PlayRightFoot() => Instance._audioSource.PlayOneShot(Instance.rightFoot);
        public static void PlayLeftFoot() => Instance._audioSource.PlayOneShot(Instance.leftFoot);
        public static void PlayJump()
        {
            if (Instance._audioSourceJump.isPlaying) return;
            Instance._audioSourceJump.Play();
        }

        public static void PlayLanding() => PlayAudio(Instance.landing);
        public static void PlayPistol() => Instance._audioSource.PlayOneShot(Instance.pistol);
        public static void PlaySniper() => Instance._audioSource.PlayOneShot(Instance.sniper);
        public static void PlayRevolver() => Instance._audioSource.PlayOneShot(Instance.revolver);
        public static void PlayCharging() => Instance._audioSource.PlayOneShot(Instance.charging);
        public static void PlayCast() => Instance._audioSource.PlayOneShot(Instance.cast);
        public static void PlayGroundPound() => Instance._audioSource.PlayOneShot(Instance.groundPound);
        public static void PlayWoosh() => Instance._audioSource.PlayOneShot(Instance.woosh);
        public static void PlayHit() => Instance._audioSource.PlayOneShot(Instance.hit);
        public static void PlayOpenUI() => Instance._audioSource.PlayOneShot(Instance.uiOpen);
        public static void StopAudio() => Instance._audioSource.Stop();

        private static void PlayAudio(AudioClip clip)
        {
            if (Instance._audioSource)
            {
                Instance._audioSource.clip = clip;
                Instance._audioSource.Play();
            }
                
        }

    }
}