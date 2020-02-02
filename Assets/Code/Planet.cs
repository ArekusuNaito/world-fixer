using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using static SharedEnums;
using static BlasterMinigame;


public class Planet : MonoBehaviour
{
    public Balance balance;
    [RangeReactiveProperty(0,100)]
    public FloatReactiveProperty health=new FloatReactiveProperty(100);
    [RangeReactiveProperty(0,1)]
    public FloatReactiveProperty charge = new FloatReactiveProperty(1);
    public enum State { CHARGING, ATTACKING, REPARING, IDLE }//WIN/LOSE

    public Text healthText;
    public Text chargeText;

    public State state=State.IDLE;
    public Minigame activeMinigame;
    public ChargingMinigame chargingMinigame;
    public BlasterMinigame attackMinigame;
    public RepairMinigame repairMinigame;
    private Planet enemy;
    
    void Awake()
    {
        this.health.SubscribeToText(healthText);
        this.charge.SubscribeToText(chargeText);
        TransitionTo(State.IDLE);
        
    }

    void Start()
    {
        this.chargingMinigame.OnPlayerInputProcessedEvent += ChargeMinigameOutput;
        this.attackMinigame.OnMinigameEndEvent+=AttackMinigameOutput;
        this.repairMinigame.OnRepairEnd+=RepairMinigameOutput;
    }

    public Planet Enemy{set{this.enemy=value;}}
    
    void ChargeMinigameOutput(InputButton pressedButton,bool success)
    {
        Debug.Assert(this.state == State.CHARGING);
        if(success)
        {
            this.AddCharge(balance.chargeAmount);
            if(IsFullCharged)
            {
                Debug.Assert(this.enemy!=null,$"{this.name} has no enemy");
                this.TransitionTo(State.ATTACKING); 
            }
        }
    }



    void AttackMinigameOutput(Result result)
    {
        var damage = TranslateAttackResultToFloat(result);
        //Animation can be placed here
        //SFX can be placed here
        enemy.Hurt(damage);
        this.enemy.TransitionTo(State.REPARING);
        this.TransitionTo(State.CHARGING);

    }

    void RepairMinigameOutput()
    {
        this.Repair(balance.repairAmount);
        this.TransitionTo(State.CHARGING);
    }

    private float TranslateAttackResultToFloat(Result attackPower)
    {
        switch (attackPower)
        {
            case Result.Perfect: return balance.perfectDamage;
            case Result.Ok: return balance.okDamage;
            case Result.Bad: return balance.badDamage;
        }
        Debug.Log("No Damage translation");
        return 0; //default is to not receive damage
    }

    void Update()
    {
        
    }
    
    public void TransitionTo(State newState)
    {
        if (state != State.IDLE) //This could potentially create issues, YET JAM
        {
            this.activeMinigame.StopMinigame();
        }
        this.state = newState;
        //Select a new active minigame
        switch(this.state)
        {
            case State.CHARGING:
            {
                this.activeMinigame = this.chargingMinigame;
                break;
            }
            case State.ATTACKING:
            {
                this.charge.Value=0;
                this.activeMinigame = this.attackMinigame;
                break;
            }
            case State.REPARING:
            {
                this.activeMinigame = this.repairMinigame;
                break;
            }
            case State.IDLE:
                {
                    this.activeMinigame = null;
                    break;
                }
        }
        //Then...
        if(state!=State.IDLE) //This could potentially create issues, YET JAM
        {
            this.activeMinigame.StartMinigame();
        }

    }

    public bool IsFullCharged{get{return this.charge.Value>=1;}}


    public void AddCharge(float value)
    {
        this.charge.Value+=value;
    }

    public void Hurt(float damage)
    {
        this.health.Value-=damage;
    }

    public void Repair(float value)
    {
        this.health.Value= this.health.Value + value >100?100:this.health.Value+value;
    }

}
