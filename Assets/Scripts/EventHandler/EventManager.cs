using UnityEngine.Events;

namespace EventHandler
{
    public static class EventManager
    {
        public static UnityEvent OnPlayerDied = new UnityEvent();
        public static UnityEvent OnPlayerDiedTriggerFast = new UnityEvent();
        public static UnityEvent OnPlayerDiedForUI = new UnityEvent();
        
        public static UnityEvent StageOneComplete = new UnityEvent();
        public static UnityEvent StageTwoComplete = new UnityEvent();
        public static UnityEvent StageThreeComplete = new UnityEvent();
        
        public static UnityEvent OnPlayerEnteredBossArea = new UnityEvent();
        public static UnityEvent OnReadyForBattle = new UnityEvent();

        public static UnityEvent OnUnlockedSniper = new UnityEvent();
        public static UnityEvent OnUnlockedPistol = new UnityEvent();
        public static UnityEvent OnUnlockedDualPistol = new UnityEvent();
        public static UnityEvent OnFirstBossDefeat = new UnityEvent();
        public static UnityEvent OnSecondBossDefeat = new UnityEvent();
        public static UnityEvent OnThirdBossDefeat = new UnityEvent();
        public static UnityEvent OnHitCompanion = new UnityEvent();
        
        public static UnityEvent OnGroundPound = new UnityEvent();

        public static UnityEvent OnBossDeath = new UnityEvent();
        public static UnityEvent ExecuteNextConvo = new UnityEvent();

        public static UnityEvent OnShowPlayerHealth = new UnityEvent();
        public static UnityEvent OnShowEnemyHealth = new UnityEvent();
        public static UnityEvent CameraShakeLow = new UnityEvent();
        public static UnityEvent CameraShakeHigh = new UnityEvent();
        public static UnityEvent SlowMo = new UnityEvent();
    }
}