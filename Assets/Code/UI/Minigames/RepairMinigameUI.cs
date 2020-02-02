using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SharedEnums;
using UnityEngine.UI;
using DG.Tweening;

public class RepairMinigameUI : MonoBehaviour
{
    [SerializeField] private GameObject m_rootObject;
    [SerializeField] private MinigameButton minigameButton;
    [SerializeField] private Image minigameImg;

    private Sequence currentBtnAnim;

    public void Show()
    {
        m_rootObject.SetActive(true);
    }

    public void Hide()
    {
        if (currentBtnAnim != null)
            currentBtnAnim.Kill();
        m_rootObject.SetActive(false);
    }

    public void OnMinigameStart(InputButton btn)
    {
        minigameButton.SetButtonImage(btn);
        ButtonAnim();
    }

    public void OnMinigameEnd()
    {
        if (currentBtnAnim != null)
            currentBtnAnim.Kill();
        minigameImg.rectTransform.localScale = Vector3.one;
    }

    private void ButtonAnim()
    {
        if (currentBtnAnim != null)
            currentBtnAnim.Kill();
        currentBtnAnim = DOTween.Sequence();
        minigameImg.rectTransform.localScale = Vector3.one;
        currentBtnAnim.Append(minigameImg.rectTransform.DOScale(1.5f, 0.1f));//staging in
        currentBtnAnim.Append(minigameImg.rectTransform.DOScale(1, 0.3f));//staging out
        currentBtnAnim.onComplete += () => ButtonAnim();
    }

    public void OnButtonTap(bool correctTap)
    {
        if(correctTap)
        {
            Spawner.Instance.Spawn(Spawner.Dude.HitBtnSuccessEffect, minigameButton.transform.position);
        }
        else
        {
            Spawner.Instance.Spawn(Spawner.Dude.HitBtnWrongEffect, minigameButton.transform.position);
        }
    }
}
