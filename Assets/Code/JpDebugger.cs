using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JpDebugger : MonoBehaviour
{
    public ChargingMinigame chargingMinigame;
    // Start is called before the first frame update
    void Start()
    {
        chargingMinigame.StartMinigame();
    }
}
