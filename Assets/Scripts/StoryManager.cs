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
    List<string> usedText = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleCompanion()
    {
        companionTalk = !companionTalk;
        flowchart.SetBooleanVariable("companionTalk", companionTalk);
        flowchart.ExecuteBlock(baseBlockName);
    }

    void ChangeStoryProgress()
    {

    }

    void GenerateCompanionText()
    {
        if (companionText.Count == 0)
        {
            companionText = usedText;
            usedText = new List<string>();
        }
        int index = Random.Range(0, companionText.Count);
        nextLine = companionText[index];
        flowchart.SetStringVariable("nextLine", nextLine);
        usedText.Add(nextLine);
        companionText.Remove(nextLine);
    }
}
