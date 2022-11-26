using System;
using Player;
using UnityEngine;

namespace StageController
{
    public class Stage1StoryProgression : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        private void Awake()
        {
            _playerController.isControllable = false;
        }


        public void ActivatePlayerControl()
        {
            _playerController.isControllable = true;
        }


    }
}