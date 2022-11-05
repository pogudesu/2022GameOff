using UnityEngine;
using UnityEngine.UI;

namespace Obvious.Soap
{
    [AddComponentMenu("Soap/Bindings/BindImageColor")]
    [RequireComponent(typeof(Image))]
    public class BindImageColor : CacheComponent<Image>
    {
        [SerializeField] private ColorVariable _colorVariable = null;
      
        protected override void Awake()
        {
            base.Awake();
            Refresh(_colorVariable.Value);
            _colorVariable.OnValueChanged += Refresh;
        }

        private void OnDestroy()
        {
            _colorVariable.OnValueChanged -= Refresh;
        }

        private void Refresh(Color color)
        {
            _component.color = color;
        }
    }
}