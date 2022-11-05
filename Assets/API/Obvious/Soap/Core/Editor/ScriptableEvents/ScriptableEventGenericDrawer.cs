using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Obvious.Soap
{
    [CustomEditor(typeof(ScriptableEventBase), true)]//TODO: to replace with ScriptableEvent<> when Odin fixes their issue
    public class ScriptableEventGenericDrawer : Editor
    {
        private MethodInfo _methodInfo;

        private void OnEnable()
        {
            _methodInfo = target.GetType().BaseType.GetMethod("Raise",
                BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            if (GUILayout.Button("Raise"))
            {
                SerializedProperty property = serializedObject.FindProperty("_debugValue");
                _methodInfo.Invoke(target, new object[1] {GetDebugValue(property)});
            }

            if (!EditorApplication.isPlaying)
                return;

            SoapInspectorUtils.DrawLine();

            var goContainer = (IDrawObjectsInInspector) target;
            var gameObjects = goContainer.GetAllObjects();

            if (gameObjects.Count > 0)
                DisplayAll(gameObjects);
        }

        private void DisplayAll(List<Object> objects)
        {
            GUILayout.Space(15);
            var title = $"Listener Amount : {objects.Count}";
            GUILayout.BeginVertical(title, "window");
            foreach (var obj in objects)
            {
                SoapInspectorUtils.DisplayObject(obj,new []{obj.name,"Select"});
            }
            GUILayout.EndVertical();
        }

        private object GetDebugValue(SerializedProperty property)
        {
            var targetType = property.serializedObject.targetObject.GetType();
            var targetField = targetType.GetField("_debugValue", BindingFlags.Instance | BindingFlags.NonPublic);
            return targetField.GetValue(property.serializedObject.targetObject);
        }
    }
}