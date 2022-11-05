using UnityEditor;
using UnityEngine;

namespace Obvious.Soap
{
    [CustomEditor(typeof(PlayModeResetter))]
    public class PlayModeResetterDrawer : Editor
    {
        private SerializedObject _serializedTargetObject;
        
        public override void OnInspectorGUI()
        {
            var color = GUI.contentColor;
            GUI.contentColor = Color.red;
            EditorGUILayout.LabelField("PlayModeResetter has to be located in a Resources folder!", EditorStyles.whiteLargeLabel);
            GUI.contentColor = color;
            SoapInspectorUtils.DrawLine();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Change the path to where are located your scriptable variables & lists", EditorStyles.miniLabel);
            
            if (_serializedTargetObject == null)
                _serializedTargetObject = new SerializedObject(target);
            
            _serializedTargetObject.DrawInspectorExcept( "m_Script");
            
            SoapInspectorUtils.DrawLine();
            if (GUILayout.Button("Get all scriptable variables at path",GUILayout.MinHeight(25)))
            {
                var variableResetter = (PlayModeResetter) target;
                variableResetter.GetAllVariablesAtPath();
            }
        }
    }
}