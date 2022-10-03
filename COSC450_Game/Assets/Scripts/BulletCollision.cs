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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision.gameObject.name);
            //find the script to take damage then destroy the bullet
            collision.gameObject.GetComponent<ZombieBehavior>().takeDamage(script.getGunDamage());
            Destroy(this.gameObject);
        }
    }
}
