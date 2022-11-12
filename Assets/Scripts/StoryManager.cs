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
    }

    void ChangeStoryProgress()
    {

    }
}
