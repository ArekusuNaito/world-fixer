using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameMaster : MonoBehaviour
{
    [Header("Game Balancing")]
    public Balance balance;

    [Header("Player Planets")]
    public Planet player1;
    public Planet player2;
    public Text lifeText;
    public Text chargeText;

    
    public enum InputResult{SUCCESS,FAILURE}
    public enum AttackPower{PERFECT,OK,MISS}
    void Start()
    {
        
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Charge(InputResult.SUCCESS,player1);
            Attack(AttackPower.OK,player2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Charge(InputResult.SUCCESS, player1);
            Attack(AttackPower.OK, player2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Charge(InputResult.SUCCESS, player2);
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

    
    public void Charge(InputResult result, Planet planet)
    {
        //minigames dont know players
        switch (result)
            {
                case InputResult.SUCCESS: //
                    {
                        Debug.Log("Charge Success");
                        planet.AddCharge(balance.chargeAmount);
                        break;
                    }
            }
    }
    public void Attack(AttackPower power,Planet targetPlanet)
    {
        var damage = TranslateAttackPowerToFloat(power);
        targetPlanet.Hurt(damage);
    }

    float TranslateAttackPowerToFloat(AttackPower attackPower)
    {
        switch (attackPower)
        {
            case AttackPower.PERFECT: return balance.perfectDamage;
            case AttackPower.OK: return balance.okDamage;
        }
        Debug.Log("No Damage translation");
        return 0; //default is to not receive damage
    }   

    void Repair(Planet player)
    {
        player.Repair(this.balance.repairAmount);
        
    }
}


