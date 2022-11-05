using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obvious.Soap
{
    class ScriptableVariableUidGenerator : AssetPostprocessor
    {
        //this gets cleared every time the domain reloads
        private static readonly HashSet<string> _guidsCache = new HashSet<string>();

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            var isInitialized = SessionState.GetBool("initialized", false);
            if (!isInitialized)
            {
                RegenerateAllGuids();
                SessionState.SetBool("initialized", true);
            }
            else
            {
                OnAssetCreated(importedAssets);
                OnAssetDeleted(deletedAssets);
                OnAssetMoved(movedFromAssetPaths, movedAssets);
            }
        }
        
        private static void RegenerateAllGuids()
        {
            var scriptableVariableBases = SoapEditorUtils.FindAll<ScriptableVariableBase>();
            foreach (var scriptableVariable in scriptableVariableBases)
            {
                scriptableVariable.Uid = GenerateUid(scriptableVariable);
                _guidsCache.Add(scriptableVariable.Uid);
                //Debug.Log(scriptableVariable.name + " was regenerated and Uid cached");
            }
        }

        private static void OnAssetCreated(string[] importedAssets)
        {
            foreach (var assetPath in importedAssets)
            {
                if (_guidsCache.Contains(assetPath))
                    continue;

                var asset = AssetDatabase.LoadAssetAtPath<ScriptableVariableBase>(assetPath);
                if (asset == null)
                    continue;

                asset.Uid = GenerateUid(asset);
                _guidsCache.Add(asset.Uid);
                //Debug.Log(asset.name + " was created and Uid cached");
            }
        }

        private static void OnAssetDeleted(string[] deletedAssets)
        {
            foreach (var assetPath in deletedAssets)
            {
                if (!_guidsCache.Contains(assetPath))
                    continue;

                _guidsCache.Remove(assetPath);
                //Debug.Log(assetPath + " was removed from cache");
            }
        }

        private static void OnAssetMoved(string[] movedFromAssetPaths, string[] movedAssets)
        {
            OnAssetDeleted(movedFromAssetPaths);
            OnAssetCreated(movedAssets);
        }

        private static string GenerateUid(ScriptableVariableBase scriptableVariableBase)
        {
            var path = AssetDatabase.GetAssetPath(scriptableVariableBase);

            if (path.Length < 310)
                return path;

            //very rare that we arrive here, but after 310 char, even Unity struggles with long paths.
            var diff = path.Length - 300; //small offset, even more safety
            var guid = path.Substring(diff); //we cut the start as several assets can be create in this folder.

            return guid;
        }
    }
}