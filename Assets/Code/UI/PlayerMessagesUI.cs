using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerMessagesUI : MonoBehaviour
{
    public GameObject textObj;
    public Text playerText;
    public DancerHelper dancer;

    Sequence seq;

    public void ShowMessage(string text)
    {
        textObj.SetActive(true);
        //
        playerText.text = text;
        dancer.StartDanceAnimation();
        //initial state
        playerText.transform.localScale = Vector3.one;
        playerText.color = Color.black;
        //
        if (seq != null)
            seq.Kill();
        seq = DOTween.Sequence();
        seq.Append(playerText.DOFade(1, 0.3f));
        seq.Insert(0,playerText.transform.DOScale(1.5f, 0.3f));
        seq.Append(playerText.transform.DOScale(1, 0.5f));
        seq.Insert(0.3f, playerText.DOFade(0, 0.5f));
        seq.onComplete = OnComplete;
    }

    private void OnComplete()
    {
        dancer.StopDanceAnimation();
        textObj.SetActive(false);
    }
}
