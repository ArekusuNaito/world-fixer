using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MinigamesConfig",menuName = "Brotherhood/MinigamesConfig", order =0)]
public class MinigamesConfig : ScriptableObject
{
    [Header("Charging Minigame")]
    public int chargingQueueSize;

    [Header("Blaster Minigame")]
    [Range(0.0f,1.0f)]
    public float maxScoreForBad;
    [Range(0.0f, 1.0f)]
    public float maxScoreForOk;
}
