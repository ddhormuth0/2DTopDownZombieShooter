using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public float bulletSpeed = 1000f;
    public GameObject bulletPrefab;
    private Transform player;
    //offset of bullet, where it will be made
    private Vector3 positionOffset;
    //multiplies to bring offset closer to player
    public float offsetModifier = .55f;
    //time left to fire
    private float fireRateCounter;
    //interval of ime between shots
    public float fireRate = .8f;
    //direction bullet will be facing
    private Quaternion fireDirection;
    // Start is called before the first frame update
    public Rigidbody2D clone;
    void Start()
    {
        player = GetComponent<Transform>();
        fireRateCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //shooting up
        if (Input.GetKey(KeyCode.I) && fireRateCounter <= 0)
        {
            //direction bullet faces
            fireDirection = Quaternion.Euler(0f, 0f, 0f);
            //set fire rate to the amount of time to wait
            fireRateCounter = fireRate;
            //offset above player
            positionOffset = player.position + Vector3.up * offsetModifier;
            //creates bullet
            var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
            //applies speed to the bullet
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletSpeed);
            //destroys bullet after 1 second
            Destroy(bullet, 1.0f);
        }
        //shooting left
        if (Input.GetKey(KeyCode.J) && fireRateCounter <= 0)
        {
            //direction bullet faces
            fireDirection = Quaternion.Euler(0f, 0f, 90f);
            //set fire rate to the amount of time to wait
            fireRateCounter = fireRate;
            //offset above player
            positionOffset = player.position + Vector3.left * offsetModifier;
            //creates bullet
            var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
            //applies speed to the bullet
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bulletSpeed);
            //destroys bullet after 1 second
            Destroy(bullet, 1.0f);
        }
        //shooting right
        if (Input.GetKey(KeyCode.L) && fireRateCounter <= 0)
        {
            //direction bullet faces
            fireDirection = Quaternion.Euler(0f, 0f, 90f);
            //set fire rate to the amount of time to wait
            fireRateCounter = fireRate;
            //offset above player
            positionOffset = player.position + Vector3.right * offsetModifier;
            //creates bullet
            var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
            //applies speed to the bullet
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * bulletSpeed);
            //destroys bullet after 1 second
            Destroy(bullet, 1.0f);
        }
        //shooting down
        if (Input.GetKey(KeyCode.K) && fireRateCounter <= 0)
        {
            //direction bullet faces
            fireDirection = Quaternion.Euler(0f, 0f, 0f);
            //set fire rate to the amount of time to wait
            fireRateCounter = fireRate;
            //offset above player
            positionOffset = player.position + Vector3.down * offsetModifier;
            //creates bullet
            var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
            //applies speed to the bullet
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.down * bulletSpeed);
            //destroys bullet after 1 second
            Destroy(bullet, 1.0f);
        }

        //countdown for timer
        if (!(fireRateCounter <= 0))
        {
            fireRateCounter -= Time.deltaTime;
        }

    }

}
