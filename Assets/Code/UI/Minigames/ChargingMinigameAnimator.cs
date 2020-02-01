using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static SharedEnums;

public class ChargingMinigameAnimator : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image nextButton;
    [SerializeField] private ChargingMinigameButton chargingMinigameButton;

    [Header("AnimaitonConfig")]
    [SerializeField] private float buttonStartPosX;
    [SerializeField] private float buttonMiddlePosX;
    [SerializeField] private float buttonStagingScale;
    [SerializeField] private float buttonEndPosX;

    //private state
    public enum State { Idle, ButtonFadeIn, ButtonStaging, ButtonFadeOut, ButtonOut };
    public enum EvaluationResult { Correct, Incorrect, Ignored };
    private State m_state = State.Idle;
    private CurrentButton m_currentButton = new CurrentButton();

    #region EVENTS FOR OUTSIDERS
    public event Action<State> OnButtonAnimationStateChanged;
    #endregion

    #region OUTSIDER API
    public void StartNextButtonAnimation(InputButton btn, float fadeInDuration, float stagingInDuration, float stagingOutDuration, float fadeOutDuration)
    {
        Debug.Assert(fadeInDuration > 0);
        Debug.Assert(stagingInDuration > 0);
        Debug.Assert(stagingOutDuration > 0);
        Debug.Assert(fadeOutDuration > 0);
        m_currentButton.btn = btn;
        m_currentButton.hasReceivedInput = false;
        chargingMinigameButton.SetButtonImage(m_currentButton.btn);
        //
        nextButton.rectTransform.anchoredPosition = new Vector2(buttonStartPosX, nextButton.rectTransform.anchoredPosition.y);
        SetState(State.ButtonFadeIn);
        Sequence animSequence = DOTween.Sequence();
        //sequence
        animSequence.Append(nextButton.rectTransform.DOAnchorPosX(buttonMiddlePosX, fadeInDuration));//fade in
        animSequence.AppendCallback(() => { SetState(State.ButtonStaging); } );
        animSequence.Append( nextButton.rectTransform.DOScale(buttonStagingScale, stagingInDuration));//staging in
        animSequence.Append( nextButton.rectTransform.DOScale(1, stagingOutDuration));//staging out
        animSequence.AppendCallback(() => { SetState(State.ButtonFadeOut); });
        animSequence.Append(nextButton.rectTransform.DOAnchorPosX(buttonEndPosX, fadeOutDuration));//fadeout
        animSequence.AppendCallback(() => { SetState(State.ButtonOut); });
    }

    //displays visual thingies and returns if it failed or succeeded
    public EvaluationResult EvaluatePlayerInput(InputButton btn)
    {
        if(m_currentButton.hasReceivedInput)
            return EvaluationResult.Ignored;

        m_currentButton.hasReceivedInput = true;
        if (m_state == State.ButtonFadeIn ||
            m_state == State.ButtonStaging ||
            m_state == State.ButtonFadeOut)
        {
            if (btn == m_currentButton.btn)
            {
                OnSuccessfulInput();
                return EvaluationResult.Correct;
            }
        }
        OnWrongInput();
        return EvaluationResult.Incorrect;
    }

    #endregion

    #region ANIMAITON AND GOODIES
    private void OnSuccessfulInput()
    {
        Spawner.Instance.Spawn(Spawner.Dude.HitBtnSuccessEffect,nextButton.transform.position);
    }
    private void OnWrongInput()
    {
        Spawner.Instance.Spawn(Spawner.Dude.HitBtnWrongEffect, nextButton.transform.position);
    }
    #endregion

    #region STATES
    private void SetState(State state)
    {
        Debug.Assert(state != State.Idle);
        //Debug.Log($"ChargingMinigameAnimator state: {state}");
        m_state = state;
        if(OnButtonAnimationStateChanged!=null)
            OnButtonAnimationStateChanged(m_state);
    }
    #endregion

    #region CURRENT BUTTON STATE
    private class CurrentButton
    {
        public InputButton btn;
        public bool hasReceivedInput;
    }
    #endregion
}
