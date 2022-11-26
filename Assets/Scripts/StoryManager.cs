using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fungus;
using EventHandler;
using Random = UnityEngine.Random;

public class StoryManager : MonoBehaviour
{
    [SerializeField]
    Flowchart flowchart;
    [SerializeField]
    Player.PlayerController pC;
    [SerializeField]bool companionTalk = false;
    int eventCounter = 0;
    [SerializeField]
    string baseBlockName = "";
    string nextLine = "";
    [SerializeField]
    List<string> companionText;
    [SerializeField]
    List<string> bossText;
    List<string> currentText;
    List<string> usedText = new List<string>();
    UnityEvent bossDeath = EventManager.OnBossDeath;
    UnityEvent enteredArena = EventManager.OnPlayerEnteredBossArea;
    UnityEvent onSniper = EventManager.OnUnlockedSniper;
    UnityEvent onDual = EventManager.OnUnlockedDualPistol;
    float delayDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        currentText = companionText;
        pC.isControllable = false;
        bossDeath.AddListener(OnBossDeath);
        enteredArena.AddListener(OnEnteredArena);
        onSniper.AddListener(ChangeStoryProgress);
        onDual.AddListener(ChangeStoryProgress);
    }

    private void OnDestroy()
    {
        bossDeath.RemoveListener(OnBossDeath);
        enteredArena.RemoveListener(OnEnteredArena);
        onSniper.RemoveListener(ChangeStoryProgress);
        onDual.RemoveListener(ChangeStoryProgress);
    }

    private void OnEnteredArena()
    {
        StartCoroutine(InitiateConvoForBossArena());
    }

    private void OnBossDeath()
    {
        StartCoroutine(InitiateConvoWhenBossDead());
    }

    IEnumerator InitiateConvoWhenBossDead()
    {
        yield return new WaitForSeconds(delayDuration);
        ExecuteNextConvo();
    }
    

    IEnumerator InitiateConvoForBossArena()
    {
        yield return new WaitForSeconds(delayDuration);
        ExecuteNextConvo();
    }

    private void InitBattle()
    {
        EventManager.OnReadyForBattle.Invoke();
    }
    
    private void ExecuteNextConvo()
    {
        ChangeStoryProgress();
        flowchart.ExecuteBlock(baseBlockName);
    }

    private void InitiateStage2BossArea()
    {
        InitiateStage();
        InitBattle();
        
    }

    private void InitiateStage()
    {
        TogglePCControl();
        ChangeStoryProgress();
        ToggleCompanionText();
    }

    void TogglePCControl()
    {
        pC.isControllable = !pC.isControllable;
    }

    void ToggleCompanion()
    {
        companionTalk = !companionTalk;
        flowchart.SetBooleanVariable("companionTalk", companionTalk);
    }

    void ToggleCompanionText()
    {
        ToggleCompanion();
        flowchart.StopAllBlocks();
        flowchart.ExecuteBlock(baseBlockName);
    }

    void ChangeStoryProgress()
    {
         eventCounter += 1;
        flowchart.SetIntegerVariable("eventCounter", eventCounter);
        if(eventCounter == 2)
        {
            SwitchTexts();
        }
        flowchart.StopAllBlocks();
        // flowchart.Get
    }

    void GenerateCompanionText()
    {
        if (currentText.Count == 0)
        {
            currentText = usedText;
            usedText = new List<string>();
        }
        int index = Random.Range(0, currentText.Count);
        nextLine = currentText[index];
        flowchart.SetStringVariable("nextLine", nextLine);
        usedText.Add(nextLine);
        companionText.Remove(nextLine);
    }

    void SwitchTexts()
    {
        currentText = bossText;
        usedText = new List<string>();
    }

    void ResetStoryProgress()
    {
        eventCounter = 0;
    }
    //ActiveControl


}
