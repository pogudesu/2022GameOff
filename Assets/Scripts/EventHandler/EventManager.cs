using UnityEngine.Events;

namespace EventHandler
{
    public static class EventManager
    {
        public static UnityEvent OnPlayerDied = new UnityEvent();
        public static UnityEvent StageOneComplete = new UnityEvent();
    }
}