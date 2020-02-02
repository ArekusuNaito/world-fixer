using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class PlanetUI : MonoBehaviour
{
    public Planet planet;
    public PlanetButtonsUI planetButtonsUI;
    public BlasterUI blasterUI;

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private DancerHelper dancerHelper;

    [Header("Slow Planet Dance")]
    [SerializeField] private DancerHelper.DanceHelperConfig slowDanceConfig;
    [SerializeField] private DancerHelper.DanceHelperConfig fastDanceConfig;

    public void OnTransitionedToState(Planet.State state)
    {
        if(state == Planet.State.REPARING)
        {
            Debug.Assert(planet.repairMinigame.IsActive);
            planetButtonsUI.StartBlinking(planet.repairMinigame.GetTargetButton());
            StartFastDance();
        }
        else
        {
            planetButtonsUI.StopBlinking();
            StartSlowDance();
        }
    }

    public void StartSlowDance()
    {
        dancerHelper.StopDanceAnimation();
        dancerHelper.StartDanceAnimation(slowDanceConfig);
    }

    public void StartFastDance()
    {
        dancerHelper.StopDanceAnimation();
        dancerHelper.StartDanceAnimation(fastDanceConfig);
    }

    public void ShootAtTarget(Vector3 targetPos, float damageToDeal)
    {
        BulletController bullet = Spawner.Instance.Spawn(Spawner.Dude.Bullet,shootingPoint.position).GetComponent<BulletController>();
        bullet.Initialize(targetPos, damageToDeal);
        //effects
        Spawner.Instance.Spawn(Spawner.Dude.BlasterShootParticles, shootingPoint.position);
    }

}
