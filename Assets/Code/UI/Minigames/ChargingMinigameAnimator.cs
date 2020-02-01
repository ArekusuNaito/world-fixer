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

    [Header("AnimaitonConfig")]
    [SerializeField] private float buttonStartPosX;
    [SerializeField] private float buttonMiddlePosX;
    [SerializeField] private float buttonStagingScale;
    [SerializeField] private float buttonEndPosX;

    //private state
    public enum State { Idle, ButtonFadeIn, ButtonStaging, ButtonFadeOut, ButtonOut };
    private State m_state = State.Idle;
    
    #region EVENTS FOR OUTSIDERS
    public event Action<State> OnButtonAnimationStateChanged;
    #endregion

    #region OUTSIDER API
    public void AnimateNextButton(float fadeInDuration, float stagingDuration, float fadeOutDuration)
    {
        Debug.Assert(fadeInDuration > 0);
        Debug.Assert(stagingDuration > 0);
        Debug.Assert(fadeOutDuration > 0);
        nextButton.rectTransform.anchoredPosition = new Vector2(buttonStartPosX, nextButton.rectTransform.anchoredPosition.y);
        SetState(State.ButtonFadeIn);
        //sequence
        Tween fadeIn = nextButton.rectTransform.DOAnchorPosX(buttonMiddlePosX, fadeInDuration);
        fadeIn.onComplete = () => { SetState(State.ButtonStaging); };
        /*Tween staging = nextButton.rectTransform.DOScale(buttonStagingScale)

        //
        Sequence animSequence = DOTween.Sequence();
        animSequence.Append(fadeIn);
        */
    }

    #endregion

    #region STATES
    private void SetState(State state)
    {
        Debug.Assert(state != State.Idle);
        m_state = state;
        if(OnButtonAnimationStateChanged!=null)
            OnButtonAnimationStateChanged(m_state);
    }
    public State GetState()
    {
        Debug.Assert(m_state != State.Idle);
        return m_state;
    }
    #endregion
}
