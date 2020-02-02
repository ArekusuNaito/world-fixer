using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class PlanetUI : MonoBehaviour
{
    public Planet planet;
    [SerializeField] private Transform shootingPoint;

    public void ShootAtTarget(Vector3 targetPos, float damageToDeal)
    {
        BulletController bullet = Spawner.Instance.Spawn(Spawner.Dude.Bullet,shootingPoint.position).GetComponent<BulletController>();
        bullet.Initialize(targetPos, damageToDeal);
        //effects
        Spawner.Instance.Spawn(Spawner.Dude.BlasterShootParticles, shootingPoint.position);
    }

}
