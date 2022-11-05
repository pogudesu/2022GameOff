using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Obvious.Soap
{
    [CustomEditor(typeof(BindComparisonToUnityEvent))]
    [CanEditMultipleObjects]
    public class BindComparisonToUnityEventDrawer : Editor
    {
        BindComparisonToUnityEvent m_targetScript;

        SerializedProperty m_boolVariable;
        SerializedProperty m_boolVariableComparer;

        SerializedProperty m_intReference;
        SerializedProperty m_intReferenceComparer;

        SerializedProperty m_floatReference;
        SerializedProperty m_floatReferenceComparer;


        SerializedProperty m_stringReference;
        SerializedProperty m_stringReferenceComparer;

        SerializedProperty m_unityEvent;

        void OnEnable()
        {
            m_targetScript = (BindComparisonToUnityEvent) target;
            m_boolVariable = serializedObject.FindProperty("m_boolVariable");
            m_boolVariableComparer = serializedObject.FindProperty("m_boolVariableComparer");

            m_intReference = serializedObject.FindProperty("m_intReference");
            m_intReferenceComparer = serializedObject.FindProperty("m_intReferenceComparer");

            m_floatReference = serializedObject.FindProperty("m_floatReference");
            m_floatReferenceComparer = serializedObject.FindProperty("m_floatReferenceComparer");

            m_stringReference = serializedObject.FindProperty("m_stringReference");
            m_stringReferenceComparer = serializedObject.FindProperty("m_stringReferenceComparer");

            m_unityEvent = serializedObject.FindProperty("m_unityEvent");
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            m_targetScript.Type = (CustomVariableType) EditorGUILayout.EnumPopup("Variable Type", m_targetScript.Type);

            switch (m_targetScript.Type)
            {
                case CustomVariableType.NONE:
                    break;
                case CustomVariableType.BOOL:
                    EditorGUILayout.PropertyField(m_boolVariable, new GUIContent("Bool Variable"));
                    EditorGUILayout.PropertyField(m_boolVariableComparer, new GUIContent("Bool Comparer"));
                    EditorGUILayout.PropertyField(m_unityEvent, new GUIContent("Event"));
                    break;
                case CustomVariableType.INT:
                    EditorGUILayout.PropertyField(m_intReference, new GUIContent("Int Reference"));
                    m_targetScript.m_operation =
                        (BindComparisonToUnityEvent.Operation) EditorGUILayout.EnumPopup("Operation",
                            m_targetScript.m_operation);
                    EditorGUILayout.PropertyField(m_intReferenceComparer, new GUIContent("Int Reference Comparer"));
                    EditorGUILayout.PropertyField(m_unityEvent, new GUIContent("Event"));
                    break;
                case CustomVariableType.FLOAT:
                    EditorGUILayout.PropertyField(m_floatReference, new GUIContent("Float Reference"));
                    m_targetScript.m_operation =
                        (BindComparisonToUnityEvent.Operation) EditorGUILayout.EnumPopup("Operation",
                            m_targetScript.m_operation);
                    EditorGUILayout.PropertyField(m_floatReferenceComparer, new GUIContent("Float Reference Comparer"));
                    EditorGUILayout.PropertyField(m_unityEvent, new GUIContent("Event"));
                    break;
                case CustomVariableType.STRING:
                    EditorGUILayout.PropertyField(m_stringReference, new GUIContent("String Reference"));
                    EditorGUILayout.PropertyField(m_stringReferenceComparer, new GUIContent("String Reference Comparer"));
                    EditorGUILayout.PropertyField(m_unityEvent, new GUIContent("Event"));
                    break;
            }

            serializedObject.ApplyModifiedProperties();
            if (EditorGUI.EndChangeCheck())
                EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }

    }
}