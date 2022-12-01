using UnityEngine;

namespace StageController
{
    public class StageThreeController : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private bool isFinalStage;

        private void Start()
        {
            if (isFinalStage == false) return;

            _playerData.isDualPistolUnlocked = true;
            _playerData.isSniperUnlocked = true;
        }
    }
}