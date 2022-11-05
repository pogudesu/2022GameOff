namespace Obvious.Soap
{
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(ScriptableVariableBase),
        true)] //TODO: to replace with ScriptableVariable<> when Odin fixes their issue
    public class ScriptableVariablePropertyDrawer : PropertyDrawer
    {
        private SerializedObject _serializedTargetObject;
        private readonly Color _bgColor = new Color(0.1f, 0.8352f, 1f, 1f);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var targetObject = property.objectReferenceValue;
            if (targetObject == null)
            {
                EditorGUI.PropertyField(position, property, label);
                EditorGUI.EndProperty();
                return;
            }

            if (_serializedTargetObject == null || _serializedTargetObject.targetObject != targetObject)
                _serializedTargetObject = new SerializedObject(targetObject);

            _serializedTargetObject.UpdateIfRequiredOrScript();

            var rect = position;
            var labelRect = position;
            labelRect.width = position.width * 0.4f; //prevent to only expand on the first half on the window

            property.isExpanded = EditorGUI.Foldout(labelRect, property.isExpanded,
                new GUIContent(""), true);

            if (property.isExpanded)
            {
                //Draw property on the full width
                rect.width = position.width;
                EditorGUI.PropertyField(rect, property, label);

                EditorGUI.indentLevel++;
                var cacheBgColor = GUI.backgroundColor;
                GUI.backgroundColor = _bgColor;
                GUILayout.BeginVertical(GUI.skin.box);
                _serializedTargetObject.DrawInspectorExcept("m_Script");
                GUI.backgroundColor = cacheBgColor;
                GUILayout.EndVertical();
                EditorGUI.indentLevel--;
            }
            else
            {
                rect.width = position.width * 0.8f;
                EditorGUI.PropertyField(rect, property, label);

                var offset = 5f;
                rect.x += rect.width + offset;
                rect.width = position.width * 0.2f - offset;
                var value = _serializedTargetObject.FindProperty("_value");
                EditorGUI.PropertyField(rect, value, GUIContent.none);
            }

            _serializedTargetObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }
    }
}