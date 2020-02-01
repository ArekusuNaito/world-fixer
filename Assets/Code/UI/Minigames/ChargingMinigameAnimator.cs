using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private State m_state = State.Idle;
    private SharedEnums.InputButton m_inputButton;
    
    #region EVENTS FOR OUTSIDERS
    public event Action<State> OnButtonAnimationStateChanged;
    #endregion

    #region OUTSIDER API
    public void StartNextButtonAnimation(SharedEnums.InputButton btn, float fadeInDuration, float stagingInDuration, float stagingOutDuration, float fadeOutDuration)
    {
        Debug.Assert(fadeInDuration > 0);
        Debug.Assert(stagingInDuration > 0);
        Debug.Assert(stagingOutDuration > 0);
        Debug.Assert(fadeOutDuration > 0);
        m_inputButton = btn;
        chargingMinigameButton.SetButtonImage(m_inputButton);
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

    #endregion

    #region STATES
    private void SetState(State state)
    {
        Debug.Assert(state != State.Idle);
        Debug.Log($"ChargingMinigameAnimator state: {state}");
        m_state = state;
        if(OnButtonAnimationStateChanged!=null)
            OnButtonAnimationStateChanged(m_state);
    }

    public State GetState()
    {
        Debug.Assert(m_state != State.Idle);
        return m_state;
    }

    public SharedEnums.InputButton GetCurrentInputButton()
    {
        Debug.Assert(m_state != State.Idle);
        return m_inputButton;
    }
    #endregion
}
