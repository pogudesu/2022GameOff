using System;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Obvious.Soap
{
    [CreateAssetMenu(menuName = "Soap/ScriptableEvents/no param")]
    public class ScriptableEventNoParam : ScriptableObject, IDrawObjectsInInspector
    {
        [SerializeField] private bool _debugLogEnabled = false;

        private readonly List<EventListenerNoParam> _eventListeners = new List<EventListenerNoParam>();
        
        public void Raise()
        {
            if (!Application.isPlaying)
                return;

            for (int i = _eventListeners.Count - 1; i >= 0; i--)
                _eventListeners[i].OnEventRaised(this, _debugLogEnabled);
        }

        public void RegisterListener(EventListenerNoParam listener)
        {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(EventListenerNoParam listener)
        {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }

        public List<Object> GetAllObjects()
        {
            var goList = new List<Object>(_eventListeners.Count);
            foreach (var eventListener in _eventListeners)
            {
                goList.Add(eventListener.gameObject);
            }
            return goList;
        }
    }
}