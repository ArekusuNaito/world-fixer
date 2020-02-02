using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
public class FixWarning : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float intervalTime=350;
    void Start()
    {
        CreateFlashInterval();
    }

    void CreateFlashInterval()
    {
        var intervalFlash = Observable.Interval(TimeSpan.FromMilliseconds(intervalTime));
        intervalFlash.Subscribe(_ =>
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }).AddTo(this);
    }

    
}
