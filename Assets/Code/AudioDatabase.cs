using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Database", menuName = "Brotherhood/Audio Database", order = 1)]
public class AudioDatabase : ScriptableObject
{   
   [Header("Charging")]
   public AudioClip chargeSuccess;
   public AudioClip chargeFail;
   public AudioClip chargeMiss;
   [Header("Attacking")]
   public AudioClip laserCharging;
   public AudioClip laserShoot;
   public AudioClip laserImpacts;
   [Header("Repairing")]
   public AudioClip repairWarning;
   public AudioClip repairA;
   public AudioClip repairComplete;
   [Header("Start/End Game")]
   public AudioClip countdownSFX;
   public AudioClip startSFX;
   public AudioClip resultsSFX;
}
