using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static SharedEnums;

public sealed class RepairMinigame : Minigame
{
    public RepairMinigameUI repairMinigameUI;

    #region EVENTS FOR OUTSIDERS
    public event Action OnRepairEnd;
    #endregion

    private InputButton targetButton;

    private enum State { Idle, Repairing };
    private State m_state = State.Idle;

    private int repairCounter;

    public override void StartMinigame()
    {
        base.StartMinigame();
        targetButton = GetRandomInputButton();
        SetState(State.Repairing);
        repairCounter = 0;
        repairMinigameUI.Show();
        repairMinigameUI.OnMinigameStart(targetButton);
        m_playerInputSender.OnButtonDownEvent += PlayerInput_OnButtonDownEvent;
    }

    public override void StopMinigame()
    {
        base.StopMinigame();
        repairMinigameUI.Hide();
        m_playerInputSender.OnButtonDownEvent -= PlayerInput_OnButtonDownEvent;
    }

    #region MINIGAME
    private void PlayerInput_OnButtonDownEvent(InputButton btn)
    {
        if (m_state == State.Repairing)
        {
            if(btn == targetButton)
            {
                repairCounter++;
                repairMinigameUI.OnButtonTap(correctTap:true);
                GameMaster.PlaySFX(GameMaster.audioDB.repairA);
                if(repairCounter>=m_minigamesConfig.requiredHitsForRepair)
                {
                    SetState(State.Idle);
                    RaiseOnRepairEnd();
                }
            }
            else
            {
                repairMinigameUI.OnButtonTap(correctTap:false);
            }
        }
    }

    private void RaiseOnRepairEnd()
    {
        repairMinigameUI.OnMinigameEnd();
        if (OnRepairEnd != null)
            OnRepairEnd();
    }
    #endregion

    private void SetState(State state)
    {
        m_state = state;
    }
}
