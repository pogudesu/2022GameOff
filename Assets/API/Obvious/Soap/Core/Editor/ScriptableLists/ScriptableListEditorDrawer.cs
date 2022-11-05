using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obvious.Soap
{
    [CustomEditor(typeof(ScriptableListBase),
        true)] //TODO: to replace with ScriptableList<> when Odin fixes their issue
    public class ScriptableListEditorDrawer : Editor
    {
        private SerializedObject _serializedTargetObject;
        private ScriptableListBase _targetScriptableList;

        public override void OnInspectorGUI()
        {
            if (_targetScriptableList == null)
                _targetScriptableList = target as ScriptableListBase;

            var isMonoBehaviour = _targetScriptableList.GetElementType.IsSubclassOf(typeof(MonoBehaviour));
            if (isMonoBehaviour)
            {
                //Do not draw the native list.
                if (_serializedTargetObject == null)
                    _serializedTargetObject = new SerializedObject(target);

                _serializedTargetObject.DrawOnlyField("m_Script", true);
                _serializedTargetObject.DrawOnlyField("_resetOn", false);
            }
            else
            {
                //we still want to display the native list for non MonoBehaviors (like SO for examples)
                DrawDefaultInspector();
            }

            if (!EditorApplication.isPlaying)
                return;

            var container = (IDrawObjectsInInspector)target;
            var gameObjects = container.GetAllObjects();

            SoapInspectorUtils.DrawLine();

            if (gameObjects.Count > 0)
                DisplayAll(gameObjects);
        }

        private void DisplayAll(List<Object> objects)
        {
            GUILayout.Space(15);
            var title = $"List Count : {objects.Count}";
            GUILayout.BeginVertical(title, "window");
            foreach (var obj in objects)
            {
                SoapInspectorUtils.DisplayObject(obj, new[] { obj.name, "Select" });
            }

            GUILayout.EndVertical();
        }
    }
}