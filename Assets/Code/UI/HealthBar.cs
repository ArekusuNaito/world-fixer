using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HealthBar : MonoBehaviour
{
    public Image fillImage;
    private float animationTime = 0.4f;

    public void Animate(float fillAmount)
    {
        fillImage.DOFillAmount(fillAmount, animationTime).SetEase(Ease.OutBounce);
    }
}
