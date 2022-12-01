using System;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StageController
{
    public class LoadScene1 : MonoBehaviour
    {
        private void Start()
        {
            AnalyticsManager.Instance.GameStarted();
        }

        public Flowchart Flowchart;
        public void LoadStage1()
        {
            Flowchart.ExecuteBlock("LoadStage1");
        }
    }
}