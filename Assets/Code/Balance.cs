using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Brotherhood/Balance", order = 1)]
public class Balance : ScriptableObject
{
    [Header("Charging State")]
    [Range(0,1)]
    public float chargeAmount=0.2f;
    [Header("Attacking State")]
    [Range(0, 1)]
    public float perfectDamage = .2f;
    [Range(0, 1)]
    public float okDamage = 0.1f;
    [Range(0, 1)]
    public float badDamage = 0.1f;

    [Header("Repair")]
    [Range(0, 1)]
    public float repairAmount=0.1f;
}
