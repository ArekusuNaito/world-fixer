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
    [RangeReactiveProperty(0,1)]
    public FloatReactiveProperty health=new FloatReactiveProperty(1);
    [RangeReactiveProperty(0,1)]
    public FloatReactiveProperty charge = new FloatReactiveProperty(1);
    public enum State { CHARGING, ATTACKING, REPARING, IDLE }//WIN/LOSE

    public HealthBar healthBar;

    public Player playerNumber;

    public State state=State.IDLE;
    public Minigame activeMinigame;
    public ChargingMinigame chargingMinigame;
    public BlasterMinigame attackMinigame;
    public RepairMinigame repairMinigame;
    private Planet enemy;
    public System.Action<Planet,Planet> GameOver;

    [SerializeField] private PlanetUI planetUI;

    void Awake()
    {
        this.health.Subscribe(newValue=>
        {
            Debug.Log($"New Value: {newValue}");
            healthBar.Animate(newValue);
        });
        TransitionTo(State.IDLE);
        
    }

    void Start()
    {
        this.chargingMinigame.OnPlayerInputProcessedEvent += ChargeMinigameOutput;
        this.attackMinigame.OnMinigameEndEvent+=AttackMinigameOutput;
        this.repairMinigame.OnRepairEnd+=RepairMinigameOutput;
        //get input
        var inputSender = GetComponent<PlayerInputSender>();
        Debug.Assert(inputSender!=null,"There's no Input sender component on the planet object");
        //Automatically add the input sender to the minigames
        this.chargingMinigame.SetInputSender = inputSender;
        this.attackMinigame.SetInputSender = inputSender;
        this.repairMinigame.SetInputSender = inputSender;
    }

    public Planet Enemy{set{this.enemy=value;}}
    
    void ChargeMinigameOutput(InputButton pressedButton,bool success)
    {
        Debug.Assert(this.state == State.CHARGING);
        if(success)
        {
            this.AddCharge(balance.chargeAmount);
            planetUI.blasterUI.SetChargeState(charge.Value);
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
        planetUI.blasterUI.SetChargeState(charge.Value);
        planetUI.ShootAtTarget(enemy.GetPlanetUI().transform.position, damage);//instad of: enemy.Hurt(damage);
        planetUI.playerMessagesUI.ShowMessage(GetAttackString(result));
        this.TransitionTo(State.CHARGING);
    }

    private string GetAttackString(Result r)
    {
        switch (r)
        {
            case Result.NoInput:
                return "Fail!";
            case Result.Bad:
                return "Bad!";
            case Result.Ok:
                return "Ok!";
            case Result.Perfect:
                return "Perfect!";
        }
        return "";
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
        if (state == newState)
            return;
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
                    repairMinigame.IamPlayer(playerNumber);
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
        planetUI.OnTransitionedToState(this.state);
    }

    public bool IsFullCharged{get{return this.charge.Value>=1;}}
    public bool IsDead{get{return this.health.Value<=0;}}

    public void AddCharge(float value)
    {
        this.charge.Value+=value;
    }

    public void Hurt(float damage)
    {
        this.health.Value-=damage;
        this.TransitionTo(State.REPARING);
        if (this.IsDead)
        {
            this.GameOver(this.enemy,this);
        }
    }

    public void Repair(float value)
    {
        this.health.Value= this.health.Value + value >100?100:this.health.Value+value;
    }

    public PlanetUI GetPlanetUI()
    {
        return planetUI;
    }

}
