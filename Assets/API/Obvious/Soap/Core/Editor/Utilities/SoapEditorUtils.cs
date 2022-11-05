using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Obvious.Soap
{
    public static class SoapEditorUtils
    {

        [MenuItem("Tools/Obvious/Soap/Delete Player Pref %#d")]
        public static void DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("<color=#52D5F2>--Player Prefs deleted--</color>");
        }

        [MenuItem("Tools/Obvious/Soap/ToggleFastPlayMode %l")]
        public static void ToggleFastPlayMode()
        {
            EditorSettings.enterPlayModeOptionsEnabled = !EditorSettings.enterPlayModeOptionsEnabled;
            AssetDatabase.Refresh();
            var text = EditorSettings.enterPlayModeOptionsEnabled ? " <color=#52D5F2>Enabled" : "<color=red>Disabled";
            text += "</color>";
            Debug.Log("Fast Play Mode " + text);
        }

        /// <summary>
        /// Find all ScriptableObjects at a certain path.
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> FindAll<T>(string path) where T : ScriptableObject
        {
            var scripts = new List<T>();
            var searchFilter = $"t:{typeof(T).Name}";
            var assetNames = AssetDatabase.FindAssets(searchFilter, new[] {path});
        
            foreach (string SOName in assetNames)
            {
                var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
                var script = AssetDatabase.LoadAssetAtPath<T>(SOpath);
                if (script == null)
                    continue;

                scripts.Add(script);
            }

            return scripts;
        }
        
        /// <summary>
        /// Find all ScriptableObjects in project
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> FindAll<T>() where T : ScriptableObject
        {
            var scripts = new List<T>();
            var searchFilter = $"t:{typeof(T).Name}";
            var assetNames = AssetDatabase.FindAssets(searchFilter);
        
            foreach (string SOName in assetNames)
            {
                var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
                var script = AssetDatabase.LoadAssetAtPath<T>(SOpath);
                if (script == null)
                    continue;

                scripts.Add(script);
            }

            return scripts;
        }

        /// <summary>
        /// Set the Game View Window to the first preset "Free Aspect".
        /// </summary>
        public static void SetGameViewScaleAndSize()
        {
            var assembly = typeof(EditorWindow).Assembly;
            var gameViewType = assembly.GetType("UnityEditor.GameView");
            var gameViewWindow = EditorWindow.GetWindow(gameViewType);

            if (gameViewWindow == null)
            {
                Debug.LogError("GameView is null!");
                return;
            }

            float defaultScale = 1f;
            var areaField = gameViewType.GetField("m_ZoomArea",
                BindingFlags.Instance | BindingFlags.NonPublic);
            var areaObj = areaField.GetValue(gameViewWindow);
            var scaleField = areaObj.GetType().GetField("m_Scale",
                BindingFlags.Instance |BindingFlags.NonPublic);
            scaleField.SetValue(areaObj, new Vector2(defaultScale, defaultScale));

            var selectedSizeIndexProp = gameViewType.GetProperty("selectedSizeIndex",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            selectedSizeIndexProp.SetValue(gameViewWindow, 0, null);
        }
    }
}