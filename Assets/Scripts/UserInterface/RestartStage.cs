using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UserInterface
{
    public class RestartStage : MonoBehaviour
    {
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnRestart);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnRestart);
        }

        private void OnEnable()
        {
            Cursor.visible = true;
        }

        private void OnRestart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}