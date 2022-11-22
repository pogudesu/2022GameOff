using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public bool isSniperUnlocked;
    public bool isDualPistolUnlocked;
    public int currentStage;
}
