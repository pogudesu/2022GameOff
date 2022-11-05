using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Obvious.Soap
{
    [CustomEditor(typeof(BindTextMeshPro))]
    [CanEditMultipleObjects]
    public class BindTextMeshProDrawer : Editor
    {
        BindTextMeshPro _targetScript;

        SerializedProperty _boolVariableProperty;
        SerializedProperty _intVariableProperty;
        SerializedProperty _floatVariableProperty;
        SerializedProperty _stringVariableProperty;

        void OnEnable()
        {
            _targetScript = (BindTextMeshPro)target;
            _boolVariableProperty = serializedObject.FindProperty("_boolVariable");
            _intVariableProperty = serializedObject.FindProperty("_intVariable");
            _floatVariableProperty = serializedObject.FindProperty("_floatVariable");
            _stringVariableProperty = serializedObject.FindProperty("_stringVariable");
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            _targetScript.Type = (CustomVariableType)EditorGUILayout.EnumPopup("Variable Type", _targetScript.Type);
            _targetScript.Prefix = EditorGUILayout.TextField(new GUIContent("Prefix", 
                "Adds a text in front of the value"), _targetScript.Prefix);
            _targetScript.Suffix = EditorGUILayout.TextField(new GUIContent("Suffix", 
                "Adds a text after the value"), _targetScript.Suffix);

            switch (_targetScript.Type)
            {
                case CustomVariableType.NONE:
                    break;
                case CustomVariableType.BOOL:
                    EditorGUILayout.PropertyField(_boolVariableProperty, new GUIContent("Bool"));
                    break;
                case CustomVariableType.INT:
                    EditorGUILayout.PropertyField(_intVariableProperty, new GUIContent("Int"));
                    _targetScript.Increment = EditorGUILayout.IntField(new GUIContent("Increment",
                            "Useful to add an offset, for example for Level counts. If your level index is  0, add 1, so it displays Level : 1"),
                        _targetScript.Increment);
                    var minMaxInt = EditorGUILayout.Vector2IntField(new GUIContent("Min Max", 
                        "Clamps the value shown to a minimum and a maximum."), _targetScript.MinMaxInt);
                    _targetScript.MinMaxInt = minMaxInt;
                    break;
                case CustomVariableType.FLOAT:
                    EditorGUILayout.PropertyField(_floatVariableProperty, new GUIContent("Float"));
                    var decimalAmount = EditorGUILayout.IntField(new GUIContent("Decimal",
                        "Round the float to a decimal"), _targetScript.DecimalAmount);
                    _targetScript.DecimalAmount = Mathf.Clamp(decimalAmount, 0, 5);
                    var minMaxFloat = EditorGUILayout.Vector2Field(new GUIContent("Min Max",
                        "Clamps the value shown to a minimum and a maximum."), _targetScript.MinMaxFloat);
                    _targetScript.MinMaxFloat = minMaxFloat;
                    break;
                case CustomVariableType.STRING:
                    EditorGUILayout.PropertyField(_stringVariableProperty, new GUIContent("String"));
                    break;
            }

            serializedObject.ApplyModifiedProperties();
            if (EditorGUI.EndChangeCheck())
                EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }
    }
}