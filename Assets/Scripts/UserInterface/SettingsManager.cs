using System;
using Audio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UserInterface
{
    public class SettingsManager : MonoBehaviour
    {
        public static float masterVol = 1f;
        public static float bgmVol = 1f;
        public static float sfxVol = 1f;
        public static bool isFullScreen = true;

        private const string MASTER_VOL = "MasterVol";
        private const string BGM_VOL = "BGMVol";
        private const string SFX_VOL = "SFXVol";
        private bool isSettingsPanelOn = false;
        [SerializeField] private GameObject settings;

        [SerializeField] private AudioMixer _audioMixer;

        public Slider masterSlider;
        public Slider bmgSlider;
        public Slider sfxSlider;


        private void Awake()
        {
            SetVolume();
            SetCursor();
        }

        private void OnEnable()
        {
            masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            bmgSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
            sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }

        private void OnDisable()
        {
            masterSlider.onValueChanged.RemoveListener(OnMasterVolumeChanged);
            bmgSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
            sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
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
            SFXController.PlayOpenUI();
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
            _audioMixer.SetFloat(MASTER_VOL, ConvertLinearToLogarithmic(masterVol));
            _audioMixer.SetFloat(BGM_VOL, ConvertLinearToLogarithmic(bgmVol));
            _audioMixer.SetFloat(SFX_VOL, ConvertLinearToLogarithmic(sfxVol));
        }
        
        private float ConvertLinearToLogarithmic(float value)
        {
            return Mathf.Log10(value) * 20;
        }
    }
}