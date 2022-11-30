using Audio;
using UnityEngine;

namespace StageController
{
    public class OutroManager : MonoBehaviour
    {
        public GameObject outroPanel;
        [SerializeField] private AudioBGMController _audioBGMController;
        private void ActivateOutro()
        {
            outroPanel.SetActive(true);
            _audioBGMController.PlayOutro();
        }
    }
}