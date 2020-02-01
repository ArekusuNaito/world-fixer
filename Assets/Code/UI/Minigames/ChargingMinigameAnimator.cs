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
    public enum State { Idle, ButtonFadeIn, ButtonFadeOut, ButtonOut };
    public enum EvaluationResult { Correct, Incorrect, Ignored };
    private State m_state = State.Idle;
    private CurrentButton m_currentButton = new CurrentButton();

    //private anim sequences
    private Sequence m_moveSequence;
    private Sequence m_pressSequence;

    #region EVENTS FOR OUTSIDERS
    public event Action<State> OnButtonAnimationStateChanged;
    #endregion

    #region OUTSIDER API
    public void StartNextButtonAnimation(InputButton btn, float fadeInDuration, float fadeOutDuration)
    {
        Debug.Assert(fadeInDuration > 0);
        Debug.Assert(fadeOutDuration > 0);
        m_currentButton.btn = btn;
        m_currentButton.hasReceivedInput = false;
        chargingMinigameButton.SetButtonImage(m_currentButton.btn);
        //
        nextButton.rectTransform.anchoredPosition = new Vector2(buttonStartPosX, nextButton.rectTransform.anchoredPosition.y);
        SetState(State.ButtonFadeIn);

        if (m_moveSequence != null)
            m_moveSequence.Kill();
        m_moveSequence = DOTween.Sequence();
        //sequence
        m_moveSequence.Append(nextButton.rectTransform.DOAnchorPosX(buttonMiddlePosX, fadeInDuration).SetEase(Ease.OutSine));//fade in
        m_moveSequence.AppendCallback(() => { SetState(State.ButtonFadeOut); });
        m_moveSequence.Append(nextButton.rectTransform.DOAnchorPosX(buttonEndPosX, fadeOutDuration).SetEase(Ease.InSine));//fadeout
        m_moveSequence.AppendCallback(() => { SetState(State.ButtonOut); });
    }

    //displays visual thingies and returns if it failed or succeeded
    public EvaluationResult EvaluatePlayerInput(InputButton btn)
    {
        if(m_currentButton.hasReceivedInput)
            return EvaluationResult.Ignored;

        m_currentButton.hasReceivedInput = true;
        if (m_state == State.ButtonFadeIn ||
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
        OnBntPressAnim();
        GameObject effect = Spawner.Instance.Spawn(Spawner.Dude.HitBtnSuccessEffect, nextButton.transform.position);
        effect.transform.SetParent(nextButton.transform);
        effect.transform.localScale = Vector3.one;
    }
    private void OnWrongInput()
    {
        OnBntPressAnim();
        GameObject effect=Spawner.Instance.Spawn(Spawner.Dude.HitBtnWrongEffect, nextButton.transform.position);
        effect.transform.SetParent(nextButton.transform);
        effect.transform.localScale = Vector3.one;
    }
    private void OnBntPressAnim()
    {
        if (m_pressSequence != null)
            m_pressSequence.Kill();
        m_pressSequence = DOTween.Sequence();
        m_pressSequence.Append(nextButton.rectTransform.DOScale(buttonStagingScale, 0.1f));//staging in
        m_pressSequence.Append(nextButton.rectTransform.DOScale(1, 0.3f));//staging out
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
