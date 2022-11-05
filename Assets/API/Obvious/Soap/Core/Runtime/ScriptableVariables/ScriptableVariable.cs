using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Obvious.Soap
{
    [Serializable]
    public abstract class ScriptableVariable<T> : ScriptableVariableBase, ISave, IReset, IDrawObjectsInInspector
    {
        [Tooltip("The value of the variable, this is changed at runtime.")] [SerializeField]
        protected T _value;

        [Tooltip("The initial value of this variable. When reset is called, it is set to this value")] [SerializeField]
        protected T _initialValue;

        [Tooltip("Log in the console whenever this variable is changed, loaded or saved.")] [SerializeField]
        private bool _debugLogEnabled = false;

        [Tooltip("If true, saves the value to Player Prefs and loads it onEnable.")] [SerializeField]
        private bool _saved = false;

        [Tooltip("Reset to initial value." +
                 " Scene Loaded : when the scene is loaded." +
                 " Application Start : Once, when the application starts.")]
        [SerializeField]
        //[ShowIf("_saved", false)]
        private ResetType _resetOn = ResetType.SceneLoaded;

        private List<Object> _listenersObjects = new List<Object>();

        private Action<T> _onValueChanged = null;

        //Register to this action if you want to be notified when this variable changes value.
        public event Action<T> OnValueChanged
        {
            add
            {
                _onValueChanged += value;

                var listener = value.Target as Object;
                if (!_listenersObjects.Contains(listener))
                    _listenersObjects.Add(listener);
            }
            remove
            {
                _onValueChanged -= value;

                var listener = value.Target as Object;
                if (_listenersObjects.Contains(listener))
                    _listenersObjects.Remove(listener);
            }
        }

        public T PreviousValue { get; private set; }

        public virtual T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value))
                    return;
                _value = value;
                ValueChanged();
            }
        }

        private void ValueChanged()
        {
            _onValueChanged?.Invoke(_value);

            if (_debugLogEnabled)
                Debug.Log(GetColorizedString());

            if (_saved)
                Save();

            PreviousValue = _value;
        }

        private void Awake()
        {
            //Prevents from resetting if no reference in a scene
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        private void OnEnable()
        {
            if (_resetOn == ResetType.SceneLoaded)
                SceneManager.sceneLoaded += OnSceneLoaded;

            Reset();
        }

        private void OnDisable()
        {
            if (_resetOn == ResetType.SceneLoaded)
                SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (mode == LoadSceneMode.Single)
                Reset();
        }
        
#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            //In non fast play mode, this get called before OnEnable(). Therefore a saved variable can get saved before loading. 
            //This check prevents the latter.
            if (Equals(_value, PreviousValue))
                return;
            ValueChanged();
        }
#endif
        public void Reset()
        {
            _listenersObjects.Clear();

            if (_saved)
                Load();
            else
            {
                Value = _initialValue;
                PreviousValue = _initialValue;
            }
        }

        public virtual void Save()
        {
            if (_debugLogEnabled)
                Debug.Log(GetColorizedString() + " <color=#52D5F2>[Saved]</color>");
        }

        public virtual void Load()
        {
            PreviousValue = _value;

            if (_debugLogEnabled)
                Debug.Log(GetColorizedString() + " <color=#52D5F2>[Loaded].</color>");
        }

        public override string ToString()
        {
            var sb = new StringBuilder(name);
            sb.Append(" : ");
            sb.Append(_value);
            return sb.ToString();
        }

        private string GetColorizedString()
        {
            return $"<color=#52D5F2>[Variable]</color> {ToString()}";
        }

        public List<Object> GetAllObjects()
        {
            return _listenersObjects;
        }
    }

    public enum ResetType
    {
        SceneLoaded,
        ApplicationStarts,
    }

    public enum CustomVariableType
    {
        NONE,
        BOOL,
        INT,
        FLOAT,
        STRING
    }
}