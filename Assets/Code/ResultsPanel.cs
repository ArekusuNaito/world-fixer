using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ResultsPanel : MonoBehaviour
{
    [Range(0,1500)]
    public float flashInterval=400;
    public GameObject player1Panel;
    public GameObject player2Panel;

    

    void Start()
    {
        CreateFlashInterval();
    }

    void CreateFlashInterval()
    {
        var intervalFlash = Observable.Interval(TimeSpan.FromMilliseconds(flashInterval))
        .DelaySubscription(TimeSpan.FromMilliseconds(flashInterval));

        intervalFlash.Subscribe(_ =>
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }).AddTo(this);
    }
}
