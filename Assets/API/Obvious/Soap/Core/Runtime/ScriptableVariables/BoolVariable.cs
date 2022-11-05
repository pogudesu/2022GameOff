﻿using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_variable_bool.asset", menuName = "Soap/ScriptableVariables/bool")]
    [System.Serializable]
    public class BoolVariable : ScriptableVariable<bool>
    {
        public override void Save()
        {
            PlayerPrefs.SetInt(this.Uid, Value ? 1 : 0);
            base.Save();
        }

        public override void Load()
        {
            Value = PlayerPrefs.GetInt(this.Uid, _initialValue ? 1 : 0) > 0;
            base.Load();
        }

        public void Toggle()
        {
            Value = !Value;
        }
    }
}