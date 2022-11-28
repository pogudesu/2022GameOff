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

        private void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}