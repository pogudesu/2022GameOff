using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class StoryManager : MonoBehaviour
{
    [SerializeField]
    Flowchart flowchart;
    bool companionTalk = false;
    int eventCounter = 0;
    string baseBlockName = "";
    string nextLine = "";
    [SerializeField]
    List<string> companionText;
    [SerializeField]
    List<string> bossText;
    List<string> currentText;
    List<string> usedText = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        currentText = companionText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    void ToggleCompanion()
    {
        companionTalk = !companionTalk;
        flowchart.SetBooleanVariable("companionTalk", companionTalk);
        flowchart.ExecuteBlock("CompanionChat");
    }

    void ChangeStoryProgress()
    {
        eventCounter += 1;
        flowchart.SetIntegerVariable("eventCounter", eventCounter);
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
}
