using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class GameMaster : MonoBehaviour
{
    [Header("Game Balancing")]
    public Balance balance;

    [Header("Player Planets")]
    public Planet player1;
    public Planet player2;
    [Header("UI")]
    public Text lifeText;
    public Text chargeText;

    
    public Text readyText;
    public GameObject readyUI;
    public enum AttackPower{PERFECT,OK,MISS}
    private int timeToStart = 3;
    void Awake()
    {
        player1.Enemy=player2;
        player2.Enemy = player1;
    }
    void Start()
    {
        var intervalObservable = Observable.Interval(TimeSpan.FromSeconds(1)).Take(4)
        .DelaySubscription(TimeSpan.FromMilliseconds(250))
        .DoOnCompleted(() =>
        {
            StartGame();
        });
        intervalObservable.Subscribe(count=>
        {
            readyText.text = timeToStart.ToString();
            //SFX GOES HERE
            timeToStart--;
        }).AddTo(this);
        
        
    }


    void StartGame()
    {
        readyUI.SetActive(false);
        player1.TransitionTo(Planet.State.CHARGING);
        player2.TransitionTo(Planet.State.CHARGING);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack(AttackPower.OK,player2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Attack(AttackPower.OK, player2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Attack(AttackPower.OK, player1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Repair(player1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Repair(player2);
        }
    }

    
    
    public void Attack(AttackPower power,Planet targetPlanet)
    {
        
        targetPlanet.Hurt(damage);
    }

      

    void Repair(Planet player)
    {
        player.Repair(this.balance.repairAmount);
        
    }
}


