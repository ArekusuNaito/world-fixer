using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HealthBar : MonoBehaviour
{
    public Image fillImage;

    void Awake()
    {
        fillImage.fillAmount=1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float newValue = fillImage.fillAmount-0.2f;
            fillImage.DOFillAmount(newValue,0.4f).SetEase(Ease.OutBounce);
        }
    }
}
