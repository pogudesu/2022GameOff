using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_variable_float.asset", menuName = "Soap/ScriptableVariables/float")]
    public class FloatVariable : ScriptableVariable<float>
    {
        [Tooltip("Clamps the value of the variable to a minimum and maximum")] 
        [SerializeField]
        private Vector2 _minMax = new Vector2(float.MinValue, float.MaxValue);

        public override void Save()
        {
            PlayerPrefs.SetFloat(this.Uid, Value);
            base.Save();
        }

        public override void Load()
        {
            Value = PlayerPrefs.GetFloat(this.Uid,_initialValue);;
            base.Load();
        }

        public void Add(float value)
        {
            Value += value;
        }

        public override float Value
        {
            get => _value;
            set
            {
                var clampedValue = Mathf.Clamp(value, _minMax.x, _minMax.y);
                base.Value = clampedValue;
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            var clampedValue = Mathf.Clamp(_value, _minMax.x, _minMax.y);
            if (_value < clampedValue || _value > clampedValue)
                _value = clampedValue;
            base.OnValidate();
        }
#endif
    }
}