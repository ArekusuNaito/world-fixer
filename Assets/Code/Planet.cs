using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    [RangeReactiveProperty(0,100)]
    public FloatReactiveProperty health=new FloatReactiveProperty(100);
    [RangeReactiveProperty(0,1)]
    public FloatReactiveProperty charge = new FloatReactiveProperty(1);
    public enum States { CHARGING, ATTACKING, REPARING, IDLE }//WIN/LOSE

    public Text healthText;
    public Text chargeText;

    void Awake()
    {
        this.health.SubscribeToText(healthText);
        this.charge.SubscribeToText(chargeText);
        
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
