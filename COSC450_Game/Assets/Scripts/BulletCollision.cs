using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    ShootingScript script;

    private void Awake()
    {
        script = GameObject.Find("Player").GetComponent<ShootingScript>();
    }
    //checks to see what the bullet collided with
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //find the script to take damage then destroy the bullet
            collision.gameObject.GetComponentInParent<ZombieBehavior>().takeDamage(script.getGunDamage());
            Destroy(this.gameObject);
        }
    }
}
