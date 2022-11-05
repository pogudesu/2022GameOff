using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Obvious.Soap
{
    [System.Serializable]
    public abstract class ScriptableEvent<T> : ScriptableEventBase, IDrawObjectsInInspector
    {  
        private readonly List<EventListenerGeneric<T>> eventListeners = new List<EventListenerGeneric<T>>();

        [SerializeField]
        private bool _debugLogEnabled = false;
        
        [SerializeField]
        protected T _debugValue = default(T);
        
        public void Raise(T param)
        {
            if (!Application.isPlaying)
                return;

            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(this, param, _debugLogEnabled);
        }

        public void RegisterListener(EventListenerGeneric<T> listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(EventListenerGeneric<T> listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }

        public List<Object> GetAllObjects()
        {
            var goList = new List<Object>(eventListeners.Count);
            foreach (var eventListener in eventListeners)
            {
                goList.Add(eventListener.gameObject);
            }
            return goList;
        }
    }
}