using UnityEditor;
using UnityEngine;

namespace Obvious.Soap
{
    /// <summary>
    /// These classes exist only to keep custom inspectors for derived types when importing the plugin Odin Inspector.
    /// Odin does not recognize custom inspector for generic class ScriptableEvent<T>, therefore we need to make a base class.
    /// TODO: when they fix the bug of detecting custom inspectors for children inheriting from generic parents, this class can be removed.
    /// </summary>
    [System.Serializable]
    public abstract class ScriptableBase : ScriptableObject
    {
    }

    [System.Serializable]
    public abstract class ScriptableVariableBase : ScriptableBase
    {
        public string Uid { get; set; } 
    }

    [System.Serializable]
    public abstract class ScriptableListBase : ScriptableBase
    {
        public virtual System.Type GetElementType { get; }
    }

    [System.Serializable]
    public abstract class ScriptableEventBase : ScriptableBase
    {
    }
}