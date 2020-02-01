using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MinigamesConfig",menuName = "Brotherhood/MinigamesConfig", order =0)]
public class MinigamesConfig : ScriptableObject
{
    [Header("Charging Minigame")]
    public int chargingQueueSize;
}
