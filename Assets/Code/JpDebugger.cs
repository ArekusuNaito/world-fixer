using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JpDebugger : MonoBehaviour
{
    public BlasterMinigame minigame;
    // Start is called before the first frame update
    void Start()
    {
        minigame.StartMinigame();
        minigame.OnMinigameEndEvent += Minigame_OnMinigameEndEvent;
    }

    private void Minigame_OnMinigameEndEvent(BlasterMinigame.Result obj)
    {
        Debug.Log($"Rsult: {obj}");
    }
}
