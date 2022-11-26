using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fungus;
using EventHandler;

public class StoryManager : MonoBehaviour
{
    [SerializeField]
    Flowchart flowchart;
    [SerializeField]
    Player.PlayerController pC;
    bool companionTalk = false;
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

    // Start is called before the first frame update
    void Start()
    {
        currentText = companionText;
        pC.isControllable = false;
        bossDeath.AddListener(ChangeStoryProgress);
        enteredArena.AddListener(ChangeStoryProgress);
        onSniper.AddListener(ChangeStoryProgress);
        onDual.AddListener(ChangeStoryProgress);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        flowchart.ExecuteBlock(baseBlockName);
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
