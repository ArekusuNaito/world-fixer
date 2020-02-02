using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SharedEnums;
using DG.Tweening;

public class BlasterMinigame : Minigame
{
    [SerializeReference] private BlasterMinigameUI blasterMinigameUI;

    private enum State { Idle, Reizing };
    public enum Result { NoInput, Bad, Ok, Perfect};
    private State m_state = State.Idle;

    //private state 
    private bool hasReceivedInput = false;
    private InputButton targetButton;

    #region EVENTS FOR OUTSIDERS
    public event Action<Result> OnMinigameEndEvent;
    #endregion

    public override void StartMinigame()
    {
        base.StartMinigame();
        hasReceivedInput = false;
        SetState(State.Reizing);
        targetButton = GetRandomInputButton();
        blasterMinigameUI.Show();
        blasterMinigameUI.StartAnimation(targetButton, OnMinigameAnimationEnd);
        m_playerInputSender.OnButtonDownEvent += PlayerInput_OnButtonDownEvent;
    }

    public override void StopMinigame()
    {
        base.StopMinigame();
        blasterMinigameUI.Hide();
        m_playerInputSender.OnButtonDownEvent += PlayerInput_OnButtonDownEvent;
    }

    #region PUBLIC INTERFACE

    private void PlayerInput_OnButtonDownEvent(InputButton btn)
    {
        if (m_state == State.Reizing)
        {
            OnPlayerInput(btn);
        }
    }

    public void OnPlayerInput(InputButton btn)
    {
        if(!hasReceivedInput)
        {
            Result res = EvaluateInput(btn);
            hasReceivedInput = true;
            RaiseMinigameEndEvent(res);
        }
    }

    private void OnMinigameAnimationEnd()
    {
        SetState(State.Idle);
        if (!hasReceivedInput)
        {//No player input when it ended?
            RaiseMinigameEndEvent(Result.NoInput);
        }
    }
    #endregion

    #region PRIVATE HELPERS
    private void SetState(State state)
    {
        m_state = state;
    }

    void PlayInputAudio(Result result)
    {
        switch(result)
        {
            case Result.Perfect: GameMaster.PlaySFX(GameMaster.audioDB.laserCharging); break;
            case Result.Ok: GameMaster.PlaySFX(GameMaster.audioDB.chargeSuccess); break;
            case Result.Bad: GameMaster.PlaySFX(GameMaster.audioDB.chargeMiss); break;
            default: GameMaster.PlaySFX(GameMaster.audioDB.chargeMiss); break;
        }
    }
    private void RaiseMinigameEndEvent(Result res)
    {
        PlayInputAudio(res);
        blasterMinigameUI.OnMinigameEnd(res);
        Debug.Log($"Blaster end:{res}");
        if (OnMinigameEndEvent != null)
            OnMinigameEndEvent(res);
    }

    private Result EvaluateInput(InputButton btn)
    {
        if (btn == targetButton)
        {
            float maxSize = blasterMinigameUI.GetMaxCircleScale();
            float currentSize = blasterMinigameUI.GetCurrentCircleScale();
            float minSize = blasterMinigameUI.GetMinCircleScale();
            Debug.Assert(minSize < maxSize);
            Debug.Assert(minSize < currentSize);
            //processing
            float modifMax = maxSize - minSize;
            float modifCur = currentSize - minSize;
            float invertedScore = modifCur / modifMax;
            Debug.Assert(invertedScore > 0 && invertedScore <= 1);
            //Calculating score
            float score = 1 - invertedScore;
            if (score <= m_minigamesConfig.maxScoreForBad)
                return Result.Bad;
            else if (score <= m_minigamesConfig.maxScoreForOk)
                return Result.Ok;
            else
                return Result.Perfect;
        }
        else
            return Result.Bad;
    }
    #endregion

}
