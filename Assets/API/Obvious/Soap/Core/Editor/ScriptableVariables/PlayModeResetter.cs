using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obvious.Soap
{
    /// <summary>
    /// This class is there to reset variables when you enable the "Editor Play Mode settings" to play faster.
    /// Because during the fast play mode, "OnEnable" on scriptable objects is not called.
    /// Therefore we need to manually reset them ! A bit annoying, but the speed gain from using the fast play mode outweight the cost.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayModeResetter.asset",
        menuName = "Soap/PlayModeResetter")]
    public class PlayModeResetter : ScriptableObject
    {
        [Tooltip("Change this to the path where are located your scriptable variables & lists")] 
        [SerializeField]
        private string _path = "Assets/Name/Soap/Examples/Content/ScriptableVariables";
        
        [Space]
        [SerializeField][Tooltip("If true, will automatically repopulate with all the variables at the path when you enter play mode.")]
        private bool _autoPopulate = true;

        [Tooltip("Serialized list so that you can have a view of all your variables")] 
        [SerializeField]
        private List<ScriptableObject> _variablesToReset = null;
        
        public void GetAllVariablesAtPath()
        {
            _variablesToReset = SoapEditorUtils.FindAll<ScriptableObject>(_path);

            for (int i = _variablesToReset.Count - 1; i >= 0; i--)
            {
                var variable = _variablesToReset[i];
                //filter variables that cannot be reset
                if (variable is IReset)
                    continue;
                _variablesToReset.RemoveAt(i);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
        
        private static void ResetVariablesManually()
        {
            var isFastPlayMode = EditorSettings.enterPlayModeOptionsEnabled;
            if (!isFastPlayMode)
                return;
            
            if (Instance._autoPopulate)
                Instance.GetAllVariablesAtPath();

            var uniqueVariablesNames = new HashSet<string>();
            foreach (var variable in Instance._variablesToReset)
            {
                if (uniqueVariablesNames.Contains(variable.name))
                {
                    Debug.LogError($"A variable with the same name already exist! Please rename this variable "+ variable.name, variable);
                }

                uniqueVariablesNames.Add(variable.name);
                (variable as IReset)?.Reset();
            }
            
            //Instance._variablesToReset.ForEach(i => (i as IReset)?.Reset());
            
            //Debug.Log("All variables were reset!");
        }
        
        #region Scriptable Singleton
        
        private static PlayModeResetter _instance;
        public static PlayModeResetter Instance => _instance;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            _instance = Resources.Load<PlayModeResetter>("PlayModeResetter") ;
            ResetVariablesManually();
        }
        
        #endregion
    }
}