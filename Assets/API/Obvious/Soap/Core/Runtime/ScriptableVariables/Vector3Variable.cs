using UnityEngine;
using UnityEngine.Events;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_variable_vector3.asset", menuName = "Soap/ScriptableVariables/vector3")]
    public class Vector3Variable : ScriptableVariable<Vector3>
    {
        public override void Save()
        {
            PlayerPrefs.SetFloat(this.Uid + "_x", Value.x);
            PlayerPrefs.SetFloat(this.Uid + "_y", Value.y);
            PlayerPrefs.SetFloat(this.Uid + "_z", Value.z);
            base.Save();
        }

        public override void Load()
        {
            var x = PlayerPrefs.GetFloat(this.Uid + "_x", _initialValue.x);
            var y = PlayerPrefs.GetFloat(this.Uid + "_y", _initialValue.y);
            var z = PlayerPrefs.GetFloat(this.Uid + "_z", _initialValue.z);
            Value = new Vector3(x,y,z);
            base.Load();
        }
    }
}