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
    [Range(0, 100)]
    public float perfectDamage = 20f;
    [Range(0, 100)]
    public float okDamage = 10f;
    [Range(0, 100)]
    public float badDamage = 10f;

    [Header("Repair")]
    [Range(0, 100)]
    public float repairAmount=8f;
}
