﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region CRAPPY SINGLETON
    public static Spawner Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public enum Dude
    {
        HitBtnSuccessEffect,
        HitBtnWrongEffect,
        BlasterShootParticles,
        SmokeImpactParticles,
        Bullet
    }

    [SerializeField] private GameObject HitBtnSuccessEffect;
    [SerializeField] private GameObject HitBtnWrongEffect;
    [SerializeField] private GameObject BlasterShootParticles;
    [SerializeField] private GameObject SmokeImpactParticles;
    [SerializeField] private GameObject Bullet;

    public GameObject Spawn(Dude dude)
    {
        return Instantiate(GetGameObject(dude));
    }

    public GameObject Spawn(Dude dude, Vector3 pos)
    {
        return Instantiate(GetGameObject(dude), pos, Quaternion.identity);
    }

    private GameObject GetGameObject(Dude dude)
    {
        switch (dude)
        {
            case Dude.HitBtnSuccessEffect:
                return HitBtnSuccessEffect;
            case Dude.HitBtnWrongEffect:
                return HitBtnWrongEffect;
            case Dude.BlasterShootParticles:
                return BlasterShootParticles;
            case Dude.SmokeImpactParticles:
                return SmokeImpactParticles;
            case Dude.Bullet:
                return Bullet;
            default:
                throw new System.NotImplementedException();
        }
    }
}
