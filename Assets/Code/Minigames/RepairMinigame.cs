using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static SharedEnums;
#if !UNITY_WEBGL
    using XInputDotNetPure;
#endif

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

    Player myPlayer;

    public override void StartMinigame()
    {
        if(!IsActive)
        {
            base.StartMinigame();
            targetButton = GetRandomInputButton();
            SetState(State.Repairing);
            repairCounter = 0;
            repairMinigameUI.Show();
            repairMinigameUI.OnMinigameStart(targetButton);
            m_playerInputSender.OnButtonDownEvent += PlayerInput_OnButtonDownEvent;
        }
    }

    public override void StopMinigame()
    {
        base.StopMinigame();
        repairMinigameUI.Hide();
        m_playerInputSender.OnButtonDownEvent -= PlayerInput_OnButtonDownEvent;
        #if !UNITY_WEBGL
            GamePad.SetVibration((PlayerIndex)myPlayer, 0, 0);
        #endif
    }

    public void IamPlayer(Player player)
    {
        this.myPlayer = player;
        #if !UNITY_WEBGL
                GamePad.SetVibration((PlayerIndex)player, 1, 1);
        #endif
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

    public InputButton GetTargetButton()
    {
        Debug.Assert(IsActive);
        return targetButton;
    }

    private void SetState(State state)
    {
        m_state = state;
    }
}
