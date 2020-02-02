using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SharedEnums;
using DG.Tweening;

public sealed class BlasterMinigameUI : MonoBehaviour
{
    [SerializeField] private GameObject m_rootObject;

    [Header("Animation Config")]
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float targetInitialScale;
    [SerializeField] private float targetEndScale;
    [SerializeField] private float scaleDuration;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private MinigameButton m_btn;
    [SerializeField] private Image m_btnImg;
    [SerializeField] private RectTransform targetImage;

    //private state
    private System.Action callback;
    private Tween targetTween;

    #region EVENTS FOR OUTSIDERS
    public void Show()
    {
        m_rootObject.SetActive(true);
    }

    public void Hide()
    {
        if (targetTween != null)
            targetTween.Kill();
        m_rootObject.SetActive(false);
    }

    public void StartAnimation(InputButton btn, System.Action onAnimationEnd)
    {
        this.callback = onAnimationEnd;
        m_btn.SetButtonImage(btn);
        AnimateCircle();
    }
    
    public void OnMinigameEnd(BlasterMinigame.Result res)
    {
        if (res == BlasterMinigame.Result.Ok || res == BlasterMinigame.Result.Perfect)
        {
            OnBntPressAnim();
            Spawner.Instance.Spawn(Spawner.Dude.HitBtnSuccessEffect, targetImage.transform.position);
        }
        else
        {
            if(res != BlasterMinigame.Result.NoInput)
            {
                OnBntPressAnim();
            }
            Spawner.Instance.Spawn(Spawner.Dude.HitBtnWrongEffect, targetImage.transform.position);
        }
    }

    private void OnBntPressAnim()
    {
        Sequence sequence = DOTween.Sequence();
        m_btnImg.rectTransform.localScale = Vector3.one;
        sequence.Append(m_btnImg.rectTransform.DOScale(1.5f, 0.1f));//staging in
        sequence.Append(m_btnImg.rectTransform.DOScale(1, 0.3f));//staging out
    }
    #endregion

    public float GetMaxCircleScale()
    {
        return targetInitialScale;
    }

    public float GetMinCircleScale()
    {
        return targetEndScale;
    }

    public float GetCurrentCircleScale()
    {
        return targetImage.transform.localScale.x;
    }

    private void AnimateCircle()
    {
        targetImage.transform.localScale = new Vector3(targetInitialScale, targetInitialScale, targetInitialScale);

        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, fadeInDuration);

        targetTween = targetImage.transform.DOScale(targetEndScale, scaleDuration);
        targetTween.SetEase(Ease.InOutSine);
        targetTween.onComplete = OnCircleScaleComplete;
    }

    private void OnCircleScaleComplete()
    {
        callback();
    }
}
