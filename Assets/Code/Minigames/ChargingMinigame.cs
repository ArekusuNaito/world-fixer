using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using static SharedEnums;

public class ChargingMinigame : Minigame
{
    [Header("References")]
    [SerializeField] private ChargingMinigameAnimator chargingMinigameAnimator;

    #region EVENTS FOR OUTSIDERS
    public event Action<InputButton, bool> OnPlayerInputProcessedEvent;
    #endregion

    #region EVENTS FOR UI
    public event Action<InputButton, float> OnInputButtonChangedEvent;
    #endregion

    private enum State { Idle, ButtonAnimating};
    //Private state
    private State m_state;
    private Queue<InputButton> m_buttonsQueue = new Queue<InputButton>();

    #region MINIGAME START/END
    public override void StartMinigame()
    {
        base.StartMinigame();
        Setup();
        ShowNextButton();
    }

    public override void StopMinigame()
    {
        base.StopMinigame();
    }
    #endregion

    #region MINIGAME SETUP
    private void Setup()
    {
        CreateInitialQueue();
    }
    private void CreateInitialQueue()
    {
        m_buttonsQueue.Clear();
        for(int x=0;x<m_minigamesConfig.chargingQueueSize;x++)
        {
            m_buttonsQueue.Enqueue(GetRandomInputButton());
        }
    }
    #endregion

    #region MINIGAME LOGIC
    private void ShowNextButton()
    {
        InputButton next = AdvanceQueue();//gets you the nws dude

    }

    private InputButton AdvanceQueue()
    {
        InputButton btn = GetNextButtonFromQueue();
        AddRandomButtonToQueue();
        Debug.Assert(m_buttonsQueue.Count == m_minigamesConfig.chargingQueueSize);
        return btn;
    }
    #endregion

    #region HELPERS FOR MINIGAME
    private InputButton GetNextButtonFromQueue()
    {
        return m_buttonsQueue.Dequeue();
    }

    private void AddRandomButtonToQueue()
    {
        m_buttonsQueue.Enqueue(GetRandomInputButton());
    }
    #endregion
}
