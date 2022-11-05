using UnityEngine.Events;
using UnityEngine;

namespace Obvious.Soap
{
    [AddComponentMenu("Soap/Bindings/BindComparisonToUnityEvent")]
    public class BindComparisonToUnityEvent : MonoBehaviour
    {
        public CustomVariableType Type = CustomVariableType.NONE;

        public BoolVariable m_boolVariable = null;
        public bool m_boolVariableComparer = false;
        
        public IntReference m_intReference = null;
        public IntReference m_intReferenceComparer = null;
        
        public FloatReference m_floatReference = null;
        public FloatReference m_floatReferenceComparer = null;
        
        public StringReference m_stringReference = null;
        public StringReference m_stringReferenceComparer = null;

        public Operation m_operation = Operation.EQUAL;
        
        [SerializeField] private UnityEvent m_unityEvent = new UnityEvent();
        
        private void Awake()
        {
            Subscribe();
        }

        private void Start()
        {
            Evaluate();
        }

        private void Evaluate()
        {
            switch (Type)
            {
                case CustomVariableType.BOOL:
                    Evaluate(m_boolVariable.Value);
                    break;
                case CustomVariableType.INT:
                    Evaluate(m_intReference.Value);
                    break;
                case CustomVariableType.FLOAT:
                    Evaluate(m_floatReference.Value);
                    break;
                case CustomVariableType.STRING:
                    Evaluate(m_stringReference.Value);
                    break;
            }
        }

        private void Evaluate(bool value)
        {
            if (value == m_boolVariableComparer)
                m_unityEvent.Invoke();
        }

        private void Evaluate(int value)
        {
            switch (m_operation)
            {
                case Operation.EQUAL:
                    if (value == m_intReferenceComparer.Value)
                        m_unityEvent.Invoke();
                    break;
                case Operation.SMALLER:
                    if (value < m_intReferenceComparer.Value)
                        m_unityEvent.Invoke();
                    break;
                case Operation.BIGGER:
                    if (value > m_intReferenceComparer.Value)
                        m_unityEvent.Invoke();
                    break;
                case Operation.BIGGER_OR_EQUAL:
                    if (value >= m_intReferenceComparer.Value)
                        m_unityEvent.Invoke();
                    break;
                case Operation.SMALLER_OR_EQUAL:
                    if (value <= m_intReferenceComparer.Value)
                        m_unityEvent.Invoke();
                    break;
            }
        }

        private void Evaluate(float value)
        {
            switch (m_operation)
            {
                case Operation.EQUAL:
                    if (Mathf.Approximately(value, m_floatReferenceComparer.Value))
                        m_unityEvent.Invoke();
                    break;
                case Operation.SMALLER:
                    if (value < m_floatReferenceComparer.Value)
                        m_unityEvent.Invoke();
                    break;
                case Operation.BIGGER:
                    if (value > m_floatReferenceComparer.Value)
                        m_unityEvent.Invoke();
                    break;
                case Operation.BIGGER_OR_EQUAL:
                    if (value >= m_floatReferenceComparer.Value)
                        m_unityEvent.Invoke();
                    break;
                case Operation.SMALLER_OR_EQUAL:
                    if (value <= m_floatReferenceComparer.Value)
                        m_unityEvent.Invoke();
                    break;
            }
        }

        private void Evaluate(string value)
        {
            if (value.Equals(m_stringReferenceComparer.Value))
                m_unityEvent.Invoke();
        }

        private void Subscribe()
        {
            switch (Type)
            {
                case CustomVariableType.BOOL:
                    m_boolVariable.OnValueChanged += Evaluate;
                    break;
                case CustomVariableType.INT:
                    m_intReference.Variable.OnValueChanged += Evaluate;
                    break;
                case CustomVariableType.FLOAT:
                    m_floatReference.Variable.OnValueChanged += Evaluate;
                    break;
                case CustomVariableType.STRING:
                    m_stringReference.Variable.OnValueChanged += Evaluate;
                    break;
            }
        }

        private void OnDestroy()
        {
            switch (Type)
            {
                case CustomVariableType.BOOL:
                    m_boolVariable.OnValueChanged -= Evaluate;
                    break;
                case CustomVariableType.INT:
                    m_intReference.Variable.OnValueChanged -= Evaluate;
                    break;
                case CustomVariableType.FLOAT:
                    m_floatReference.Variable.OnValueChanged -= Evaluate;
                    break;
                case CustomVariableType.STRING:
                    m_stringReference.Variable.OnValueChanged -= Evaluate;
                    break;
            }
        }

        public enum Operation
        {
            EQUAL,
            SMALLER,
            BIGGER,
            BIGGER_OR_EQUAL,
            SMALLER_OR_EQUAL
        }
    }
}