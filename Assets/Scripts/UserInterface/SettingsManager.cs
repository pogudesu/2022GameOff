using System;
using UnityEngine;
using UnityEngine.Audio;

namespace UserInterface
{
    public class SettingsManager : MonoBehaviour
    {
        public static float masterVol = 0.5f;
        public static float bgmVol = 0.5f;
        public static float sfxVol = 0.5f;
        public static bool isFullScreen = true;

        private const string MASTER_VOL = "MasterVol";
        private const string BGM_VOL = "BGMVol";
        private const string SFX_VOL = "SFXVol";
        private bool isSettingsPanelOn = false;
        [SerializeField] private GameObject settings;

        [SerializeField] private AudioMixer _audioMixer;


        private void Awake()
        {
            SetVolume();
            SetCursor();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TriggerSettingsPanel();
            }
        }

        private void SetCursor()
        {
            Cursor.visible = isSettingsPanelOn;
        }

        public void TriggerSettingsPanel()
        {
            isSettingsPanelOn = !isSettingsPanelOn;
            if (isSettingsPanelOn == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            settings.SetActive(isSettingsPanelOn);
            SetCursor();
        }
        public void OnMasterVolumeChanged(float value)
        {
            masterVol = value;
            SetVolume();
        }

        public void OnBGMVolumeChanged(float value)
        {
            bgmVol = value;
            SetVolume();
        }

        public void OnSFXVolumeChanged(float value)
        {
            sfxVol = value;
            SetVolume();
        }

        public void OnChangeFullscreen(bool isFull)
        {
            isFullScreen = isFull;
            Screen.fullScreen = isFullScreen;
        }

        private void SetVolume()
        {
            _audioMixer.SetFloat(MASTER_VOL, masterVol);
            _audioMixer.SetFloat(BGM_VOL, bgmVol);
            _audioMixer.SetFloat(SFX_VOL, sfxVol);
        }
        
        private float ConvertLinearToLogarithmic(float value)
        {
            return Mathf.Log10(value) * 20;
        }
    }
}