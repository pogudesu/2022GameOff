using UnityEngine.Events;

namespace EventHandler
{
    public static class EventManager
    {
        public static UnityEvent OnPlayerDied = new UnityEvent();
        
        public static UnityEvent StageOneComplete = new UnityEvent();
        public static UnityEvent StageTwoComplete = new UnityEvent();
        public static UnityEvent StageThreeComplete = new UnityEvent();
        
        public static UnityEvent OnPlayerEnteredBossArea = new UnityEvent();
        public static UnityEvent OnReadyForBattle = new UnityEvent();
    }
}