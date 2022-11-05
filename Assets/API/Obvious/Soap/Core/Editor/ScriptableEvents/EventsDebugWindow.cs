using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Obvious.Soap
{
    public class EventsDebugWindow : EditorWindow
    {
        [MenuItem("Window/Obvious/Soap/Event Debug Window")]
        public new static void Show()
        {
            GetWindow<EventsDebugWindow>("Events Debug Window");
        }

        private string _methodName = string.Empty;

        private void OnGUI()
        {
            _methodName = EditorGUILayout.TextField("Method Name", _methodName);

            if (GUILayout.Button("Find"))
            {
                FindMethod(_methodName);
            }
        }

        private void FindMethod(string methodName)
        {
            var eventListeners = FindAllInOpenScenes<EventListenerBase>();

            var found = false;
            foreach (var listener in eventListeners)
            {
                if (listener.ContainsCallToMethod(methodName))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Debug.Log("<color=#52D5F2>" + methodName + "()</color>" + " could not be found in the listeners in the scene");
            }
        }

        private static List<T> FindAllInOpenScenes<T>()
        {
            List<T> results = new List<T>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var s = SceneManager.GetSceneAt(i);
                if (s.isLoaded)
                {
                    var allGameObjects = s.GetRootGameObjects();
                    for (int j = 0; j < allGameObjects.Length; j++)
                    {
                        var go = allGameObjects[j];
                        results.AddRange(go.GetComponentsInChildren<T>(true));
                    }
                }
            }

            return results;
        }
    }
}