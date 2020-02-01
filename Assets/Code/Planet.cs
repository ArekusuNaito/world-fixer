using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using static SharedEnums;

public class Planet : MonoBehaviour
{
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
    public Minigame attackMinigame;
    public Minigame repairMinigame;
    
    void Awake()
    {
        this.health.SubscribeToText(healthText);
        this.charge.SubscribeToText(chargeText);
        TransitionTo(State.IDLE);
        
    }

    void Start()
    {
        // this.chargingMinigame.OnPlayerInputProcessedEvent+=Something;
        

    }

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //Force charging minigame
        {
            Debug.Log("Forcing charge minigame");
            TransitionTo(State.CHARGING);
        }
        if(Input.GetKeyDown(KeyCode.W)) //Force attack minigame
        {
            Debug.Log("Forcing attack minigame");
            TransitionTo(State.ATTACKING);
        }
        if (Input.GetKeyDown(KeyCode.E)) //Force repair minigame
        {
            Debug.Log("Forcing repair minigame");
            TransitionTo(State.REPARING);
        }
    }
    
    void TransitionTo(State newState)
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
