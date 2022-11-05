using UnityEngine;
using UnityEngine.UI;

namespace Obvious.Soap
{
    [AddComponentMenu("Soap/Bindings/BindSlider")]
    [RequireComponent(typeof(Slider))]
    public class BindSlider : CacheComponent<Slider>
    {
        [SerializeField] private FloatVariable _boundVariable = null;

        protected override void Awake()
        {
            base.Awake();
            OnValueChanged(_boundVariable.Value);
            _component.onValueChanged.AddListener(SetBoundVariable);
            _boundVariable.OnValueChanged += OnValueChanged;
        }

        private void OnDestroy()
        {
            _component.onValueChanged.RemoveListener(SetBoundVariable);
            _boundVariable.OnValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(float value)
        {
            _component.value = value;
        }

        private void SetBoundVariable(float value)
        {
            _boundVariable.Value = value;
        }
    }
}