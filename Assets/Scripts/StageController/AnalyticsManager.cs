using System;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace StageController
{
    public class AnalyticsManager : MonoBehaviour
    {
        public static AnalyticsManager Instance;
        
        // Event
        private const string MISSION_START = "Cus_Mission_Start";
        private const string PLAYER_DIED = "Cus_Player_Died";
        private const string FIRST_BOSS_DEFEAT = "CUS_FIRST_BOSS_DEFEAT";
        private const string SECOND_BOSS_DEFEAT = "CUS_SECOND_BOSS_DEFEAT";
        private const string THIRD_BOSS_DEFEAT = "CUS_THIRD_BOSS_DEFEAT";
        private const string LAST_BOSS_DEFEAT = "CUS_LAST_BOSS_DEFEAT";
        private const string SHOOT_DECISION = "CUS_SHOOT_DECISION";
        private const string FORGIVE_DECISION = "CUS_FORGIVE_DECISION";
        private const string GAME_QUIT = "Cus_Game_Quit";

        private string timeSpentParams = "Cus_Time_Spent";

        
        
        private async void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
                
                var options = new InitializationOptions();
                options.SetEnvironmentName("production");
                await UnityServices.InitializeAsync(options);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void GameStarted()
        {

            SendCustomEvent(MISSION_START);
        }
        
        public void PlayerDied()
        {

            SendCustomEvent(PLAYER_DIED);
        }
        
        public void FirstBossDefeat()
        {

            SendCustomEvent(FIRST_BOSS_DEFEAT);
        }
        
        public void SecondBossDefeat()
        {

            SendCustomEvent(SECOND_BOSS_DEFEAT);
        }
        
        public void ThirdBossDefeat()
        {

            SendCustomEvent(THIRD_BOSS_DEFEAT);
        }
        
        public void LastBossDefeat()
        {

            SendCustomEvent(LAST_BOSS_DEFEAT);
        }
        
        public void OnShoot()
        {

            SendCustomEvent(SHOOT_DECISION);
        }
        
        public void OnForgive()
        {

            SendCustomEvent(FORGIVE_DECISION);
        }
        

        private void SendCustomEvent(string eventName)
        {
            
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
            };
            AnalyticsService.Instance.CustomData(eventName, parameters);
            Debug.Log("Analytics data Successfully sent " + eventName);
        }

        private void OnApplicationQuit()
        {
            // float time = Time.realtimeSinceStartup / 60f;
            // Dictionary<string, object> parameters = new Dictionary<string, object>()
            // {
            //     { timeSpentParams,  Math.Round(time, 2)}
            // };
            // SendCustomEvent(GAME_QUIT, parameters);
        }
    }
}