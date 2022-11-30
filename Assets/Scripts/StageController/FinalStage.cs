using System;
using System.Collections;
using Audio;
using Enemy;
using EventHandler;
using Player;
using UnityEngine;

namespace StageController
{
    public class FinalStage : MonoBehaviour
    {
        [SerializeField] private StoryManager _storyManager;
        [SerializeField] private GameObject decisionUIPanel;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Animator EnemyAnimator;
        [SerializeField] private CompanionBossController lastBoss;
        public StageController stageController;
        private void OnEnable()
        {
            EventManager.OnBossDeath.AddListener(OnBossDeath);
        }

        private void OnDisable()
        {
            EventManager.OnBossDeath.RemoveListener(OnBossDeath);
        }

        private void OnBossDeath()
        {
            playerController.isControllable = false;
            if (decisionUIPanel)
            {
                if(stageController)
                    stageController.isOnCutScene = true;
                Cursor.visible = true;
                decisionUIPanel.SetActive(true);
            }
        }

        public void OnShootDecision()
        {
            playerController.CutSceneFinalDecisionShoot();
            StartCoroutine(EnemyDeflect());
        }

        IEnumerator EnemyDeflect()
        {
            lastBoss.dashDamage = 1000;
            yield return new WaitForSeconds(3f);
            EnemyAnimator.SetTrigger("FlyProjectile");
        }
        
        
        

    }
}