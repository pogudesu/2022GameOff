using UnityEngine;

namespace StageController
{
    public class OutroManager : MonoBehaviour
    {
        public GameObject outroPanel;

        private void ActivateOutro()
        {
            outroPanel.SetActive(true);
        }
    }
}