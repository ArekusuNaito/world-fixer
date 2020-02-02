using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class PlanetButtonUI : MonoBehaviour
{
    [SerializeField] private Image btnImg;
    [SerializeField] private Color btnColor;

    [Header("AnimConfig")]
    [SerializeField] private float blinkDuration;

    //private state
    private Sequence sequence;
    private bool isBlinking = false;

    public void StartBlinkAnimation()
    {
        if(!isBlinking)
        {
            isBlinking = true;
            StopSequence();
            StartSequence();
        }
    }

    public void StopBlinkAnimation()
    {
        if (isBlinking)
        {
            isBlinking = false;
            StopSequence();
            btnImg.color = Color.white;
        }
    }

    //cycles itself
    private void StartSequence()
    {
        sequence = DOTween.Sequence();
        btnImg.color = Color.white;
        sequence.Append(btnImg.DOColor(btnColor, blinkDuration));
        sequence.Append(btnImg.DOColor(Color.white, blinkDuration));
        sequence.onComplete = () => StartSequence();
    }

    private void StopSequence()
    {
        if (sequence != null)
            sequence.Kill();
        sequence = null;
    }

}
