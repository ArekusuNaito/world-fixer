using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float angularSpeed;
    [SerializeField] private Rigidbody2D rigidBody;

    private float damageToDeal;

    public void Initialize(Vector3 targetPos, float damageToDeal)
    {
        this.damageToDeal = damageToDeal;
        rigidBody.angularVelocity = angularSpeed;
        rigidBody.velocity = (-transform.position + targetPos) * speed;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Planet"))
        {
            PlanetUI p = collision.gameObject.GetComponent<PlanetUI>();
            //DO DAMAGE AND SO ON!
            p.planet.Hurt(damageToDeal);
            //Spawn effects & destroy myselfa
            Spawner.Instance.Spawn(Spawner.Dude.SmokeImpactParticles, transform.position);
            Destroy(this.gameObject);
        }
    }
}
