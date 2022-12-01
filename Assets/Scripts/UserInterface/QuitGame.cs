using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UserInterface
{
    public class QuitGame : MonoBehaviour
    {
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnQuit);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnQuit);
        }

        private void OnQuit()
        {
            Application.Quit();
        }
    }
}