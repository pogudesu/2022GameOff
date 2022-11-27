using System;
using EventHandler;
using Player;
using UnityEngine;

namespace StageController
{
    public class FinalStage : MonoBehaviour
    {
        [SerializeField] private StoryManager _storyManager;
        [SerializeField] private GameObject decisionUIPanel;
        [SerializeField] private PlayerController playerController;
        private void OnEnable()
        {
            EventManager.OnBossDeath.AddListener(OnBossDeath);
        }

        private void OnDisable()
        {
            EventManager.OnBossDeath.RemoveListener(OnBossDeath);
        }

        private void OnBossDeath()
        {
            playerController.isControllable = false;
            if (decisionUIPanel)
            {
                decisionUIPanel.SetActive(true);
            }
        }

    }
}