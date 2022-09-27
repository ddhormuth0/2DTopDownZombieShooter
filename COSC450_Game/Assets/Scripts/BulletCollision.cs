using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    ShootingScript script;

    private void Start()
    {
        script = GameObject.Find("Player").GetComponent<ShootingScript>();
    }
    //checks to see what the bullet collided with
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //find the script to take damage then destroy the bullet
            var zScript = collision.gameObject.GetComponent<ZombieBehavior>();
            zScript.takeDamage(script.getGunDamage());
            Destroy(this.gameObject);
        }
    }
}
