using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_variable_int.asset", menuName = "Soap/ScriptableVariables/int")]
    public class IntVariable : ScriptableVariable<int>
    {
        [Tooltip("Clamps the value of the variable to a minimum and maximum")] [SerializeField]
        private Vector2Int _minMax = new Vector2Int(int.MinValue, int.MaxValue);

        public override void Save()
        {
            PlayerPrefs.SetInt(this.Uid, Value);
            base.Save();
        }

        public override void Load()
        {
            Value = PlayerPrefs.GetInt(this.Uid,_initialValue);
            base.Load();
        }

        public void Add(int value)
        {
            Value += value;
        }

        public override int Value
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