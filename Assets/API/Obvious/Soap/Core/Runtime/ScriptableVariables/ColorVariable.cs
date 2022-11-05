using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_variable_color.asset", menuName = "Soap/ScriptableVariables/color")]
    public class ColorVariable : ScriptableVariable<Color>
    {
        public override void Save()
        {
            PlayerPrefs.SetFloat(this.Uid + "_r", Value.r);
            PlayerPrefs.SetFloat(this.Uid + "_g", Value.g);
            PlayerPrefs.SetFloat(this.Uid + "_b", Value.b);
            PlayerPrefs.SetFloat(this.Uid + "_a", Value.a);
            base.Save();
        }

        public override void Load()
        {
            var r = PlayerPrefs.GetFloat(this.Uid + "_r", _initialValue.r);
            var g = PlayerPrefs.GetFloat(this.Uid + "_g", _initialValue.g);
            var b = PlayerPrefs.GetFloat(this.Uid + "_b", _initialValue.b);
            var a = PlayerPrefs.GetFloat(this.Uid + "_a", _initialValue.a);
            Value = new Color(r, g, b, a);
            base.Load();
        }

        public void SetRandom()
        {
            var beautifulColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            Value = beautifulColor;
        }
    }
}