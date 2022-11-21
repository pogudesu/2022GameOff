using UnityEngine;

namespace StageController
{
    public class StageThreeController : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;

        private void Start()
        {
            _playerData.currentStage += 1;
            _playerData.isDualPistolUnlocked = true;
        }
    }
}