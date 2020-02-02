using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JpDebugger : MonoBehaviour
{
    public RepairMinigame minigame;
    // Start is called before the first frame update
    void Start()
    {
        minigame.StartMinigame();
        minigame.OnRepairEnd += Minigame_OnRepairEnd;
    }

    private void Minigame_OnRepairEnd()
    {
        Debug.Log("repari end");
    }
    
}
