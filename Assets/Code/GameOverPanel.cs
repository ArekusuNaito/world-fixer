using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameOverPanel : MonoBehaviour
{
    [Range(0,1500)]
    public float flashInterval=400;
    public Text restartText;
    public Image player1Icon;
    public Image player2Icon;
    public Sprite winnerIcon;
    public Sprite loserIcon;
    public float animationTime = 2f;

    public void Execute()
    {
        this.gameObject.SetActive(true);
    }    

    public void SetLeftRightIcons()
    {
        player1Icon.sprite = winnerIcon;
        player2Icon.sprite = loserIcon;
    }
    public void SetRightLeft()
    {
        player1Icon.sprite = loserIcon;
        player2Icon.sprite = winnerIcon;
    }
    

    void Start()
    {
        
        player1Icon.rectTransform.DOLocalMove(new Vector3(0,0,0),2f).SetEase(Ease.OutBounce);
        player2Icon.rectTransform.DOLocalMove(new Vector3(0, 0, 0), 2f).SetEase(Ease.OutBounce);
        CreateFlashInterval();
    }

    void CreateFlashInterval()
    {
        var intervalFlash = Observable.Interval(TimeSpan.FromMilliseconds(flashInterval))
        .DelaySubscription(TimeSpan.FromMilliseconds(animationTime*1000));
        intervalFlash.Subscribe(_ =>
        {
            this.restartText.gameObject.SetActive(!this.restartText.gameObject.activeSelf);
        }).AddTo(this);
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }
}
