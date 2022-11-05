using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obvious.Soap
{
    [CustomEditor(typeof(ScriptableVariable<>), true)]
    public class ScriptableVariableDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (!EditorApplication.isPlaying)
                return;

            var container = (IDrawObjectsInInspector) target;
            var objects = container.GetAllObjects();

            SoapInspectorUtils.DrawLine();

            if (objects.Count > 0)
                DisplayAll(objects);
        }

        private void DisplayAll(List<Object> objects)
        {
            GUILayout.Space(15);
            var title = $"Objects reacting to OnValueChanged Event : {objects.Count}";
            GUILayout.BeginVertical(title, "window");
            foreach (var obj in objects)
            {
                var text = $"{obj.name}  ({obj.GetType().Name})";
                SoapInspectorUtils.DisplayObject(obj, new []{text,"Select"});
            }
            GUILayout.EndVertical();
        }
    }
}