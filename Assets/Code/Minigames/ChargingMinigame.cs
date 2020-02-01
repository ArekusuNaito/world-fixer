using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using static SharedEnums;

public class ChargingMinigame : Minigame
{
    [Header("References")]
    [SerializeField] private ChargingMinigameAnimator m_chargingMinigameAnimator;

    #region EVENTS FOR OUTSIDERS
    public event Action<InputButton, bool> OnPlayerInputProcessedEvent;
    #endregion

    private enum State { Idle, ButtonAnimating };
    //Private state
    private State m_state;
    private Queue<InputButton> m_buttonsQueue = new Queue<InputButton>();

    #region MINIGAME START/END
    public override void StartMinigame()
    {
        base.StartMinigame();
        Setup();
        ShowNextButton();
        m_chargingMinigameAnimator.OnButtonAnimationStateChanged += ChargingMinigameAnimator_OnButtonAnimationStateChanged;
        m_playerInputSender.OnButtonDownEvent += PlayerInput_OnButtonDownEvent;
    }

    public override void StopMinigame()
    {
        base.StopMinigame();
        SetState(State.Idle);
        m_chargingMinigameAnimator.OnButtonAnimationStateChanged -= ChargingMinigameAnimator_OnButtonAnimationStateChanged;
        m_playerInputSender.OnButtonDownEvent -= PlayerInput_OnButtonDownEvent;
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
        SetState(State.ButtonAnimating);
        InputButton next = AdvanceQueue();//gets you the nws dude
        m_chargingMinigameAnimator.StartNextButtonAnimation(0.5f, 0.3f, 0.1f, 0.5f);
    }

    private void ChargingMinigameAnimator_OnButtonAnimationStateChanged(ChargingMinigameAnimator.State state)
    {
        Debug.Assert(m_state == State.ButtonAnimating);
        switch (state)
        {
            case ChargingMinigameAnimator.State.ButtonFadeIn:
                break;
            case ChargingMinigameAnimator.State.ButtonStaging:
                break;
            case ChargingMinigameAnimator.State.ButtonFadeOut:
                break;
            case ChargingMinigameAnimator.State.ButtonOut:
                {//Button Animation ended
                    ShowNextButton();
                }
                break;
        }
    }

    private void PlayerInput_OnButtonDownEvent(InputButton btn)
    {
        if(m_state == State.ButtonAnimating)
        {
            bool success = IsPlayerInputCorrect(btn, m_chargingMinigameAnimator.GetCurrentInputButton(), m_chargingMinigameAnimator.GetState());
            if (OnPlayerInputProcessedEvent != null)
                OnPlayerInputProcessedEvent(btn, success);
        }
    }

    private InputButton AdvanceQueue()
    {
        InputButton btn = GetNextButtonFromQueue();
        AddRandomButtonToQueue();
        Debug.Assert(m_buttonsQueue.Count == m_minigamesConfig.chargingQueueSize);
        return btn;
    }

    private void SetState(State state)
    {
        m_state = state;
    }
    #endregion

    #region HELPERS FOR MINIGAME
    private bool IsPlayerInputCorrect(InputButton btn, InputButton targetBtn, ChargingMinigameAnimator.State animatorState)
    {
        if(animatorState == ChargingMinigameAnimator.State.ButtonFadeIn ||
            animatorState == ChargingMinigameAnimator.State.ButtonStaging || 
            animatorState == ChargingMinigameAnimator.State.ButtonFadeOut)
        {
            if(btn == targetBtn)
            {
                return true;
            }
        }
        return false;
    }

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
