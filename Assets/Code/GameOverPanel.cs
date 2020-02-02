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
    public Text player1ResultText;
    public Text player2ResultText;
    public Sprite winnerIcon;
    public Sprite loserIcon;
    public Color winnerColor;
    public Color loserColor;
    public float animationTime = 2f;

    public void Execute()
    {
        this.gameObject.SetActive(true);

    }    

    public void SetLeftRightIcons()
    {
        //P1
        player1Icon.sprite = winnerIcon;
        player1ResultText.text =  "WIN!";
        player1ResultText.color = winnerColor;

        //P2
        player2Icon.sprite = loserIcon;
        player2ResultText.text = "LOSE!";
        player2ResultText.color = loserColor;
    }
    public void SetRightLeft()
    {
        player1Icon.sprite = loserIcon;
        player1ResultText.text = "LOSE!";
        player1ResultText.color = loserColor;
        //
        player2Icon.sprite = winnerIcon;
        player2ResultText.text = "WIN!";
        player2ResultText.color = winnerColor;
    }
    

    void Start()
    {
        
        player1Icon.rectTransform.DOLocalMove(new Vector3(0,0,0),animationTime).SetEase(Ease.OutBounce);
        player2Icon.rectTransform.DOLocalMove(new Vector3(0, 0, 0), animationTime).SetEase(Ease.OutBounce);
        CreateFlashInterval();
        Observable.Timer(TimeSpan.FromSeconds(animationTime)).Subscribe(_=>
        {
            Observable.EveryUpdate().Where(__=>Input.anyKeyDown).Take(1).Subscribe(___=>
            {
                SceneManager.LoadScene(0);
            }).AddTo(this);            
        }).AddTo(this);
        
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
        
    }
}
