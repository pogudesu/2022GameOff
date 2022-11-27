using EventHandler;
using UnityEngine;

namespace Enemy
{
    public class FinalBossEntranceApperance : MonoBehaviour
    {
        
        public void OnEntered()
        {
            EventManager.ExecuteNextConvo.Invoke();
        }
    }
}