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
    //time for a powerups duration
    public float gunSpeedTimerDuration = 20f;
    public float movementSpeedTimerDuration = 20f;
    //timers for the powerups
    private float gunSpeedTimer;
    private float movementSpeedTimer;
    //shows if their was a power up
    private bool powerUpGun;
    private bool powerUpSpeed;
    //allows you to wait to use
    private bool gunPowerUpReady;
    private bool speedPowerUpReady;
    //gun damage
    private float gunDamage;

    [SerializeField] float gunOffset = .5f;
    [SerializeField] float gunModifier;
    [SerializeField] float movementSpeedModifier;
    [SerializeField] AudioSource gunNoise;

    private PlayerMovement playerMovement;

    void Start()
    {
        gunDamage = 1f;
        powerUpGun = false;
        powerUpSpeed = false;
        gunSpeedTimer = 0f;
        movementSpeedTimer = 0f;
        playerMovement = GetComponent<PlayerMovement>();
        player = GetComponent<Transform>();
        fireRateCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
      
        //when the timers are up then you go back to normal, also checks if there was a powerup given
        if (gunSpeedTimer <= 0 && powerUpGun)
        {
            fireRate *= gunModifier;
            powerUpGun = false;
        }
        //if the timer is up then reduce it
        if (movementSpeedTimer <= 0 && powerUpSpeed)
        {
            playerMovement.playerSpeed /= movementSpeedModifier;
            powerUpSpeed = false;
        }
        //count downs
        if (!(fireRateCounter <= 0))
        {
            fireRateCounter -= Time.deltaTime;
        }
        if (!(movementSpeedTimer <= 0))
        {
            movementSpeedTimer -= Time.deltaTime;
        }
        if (!(gunSpeedTimer <= 0))
        {
            gunSpeedTimer -= Time.deltaTime;
        }

    }

    void FixedUpdate()
    {
        //shooting up
        if (Input.GetKey(KeyCode.I) && fireRateCounter <= 0)
        {
            //shoot top right
            if (Input.GetKey(KeyCode.L))
            {
                //player faces this direction
                player.rotation = Quaternion.Euler(0f, 0f, -45f);
                //direction bullet faces
                fireDirection = Quaternion.Euler(0f, 0f, -45f);
                //set fire rate to the amount of time to wait
                fireRateCounter = fireRate;
                //offset so bullet doesn't spawn on player, diagonols have less offset
                positionOffset = player.position + (Vector3.up + Vector3.right) * offsetModifier * .16f + Vector3.right * gunOffset*1.3f;
                //creates bullet
                var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
                gunNoise.Play();
                //applies speed to the bullet
                bullet.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.right) * bulletSpeed);
                //destroys bullet after 1 second
                Destroy(bullet, 1.0f);
                
            }
            //shoot top left
            else if (Input.GetKey(KeyCode.J))
            {
                //player faces this direction
                player.rotation = Quaternion.Euler(0f, 0f, 45f);
                //direction bullet faces
                fireDirection = Quaternion.Euler(0f, 0f, 45f);
                //set fire rate to the amount of time to wait
                fireRateCounter = fireRate;
                //offset so bullet doesn't spawn on player, diagonols have less offset
                positionOffset = player.position + (Vector3.up*1.2f + Vector3.left*.9f) * offsetModifier + Vector3.right * gunOffset;
                //creates bullet
                var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
                gunNoise.Play();
                //applies speed to the bullet
                bullet.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.left) * bulletSpeed);
                //destroys bullet after 1 second
                Destroy(bullet, 1.0f);
            }
            //shoot up
            else
            {
                //player faces this direction
                player.rotation = Quaternion.Euler(0f, 0f, 0f);
                //direction bullet faces
                fireDirection = Quaternion.Euler(0f, 0f, 0f);
                //set fire rate to the amount of time to wait
                fireRateCounter = fireRate;
                //offset below player
                positionOffset = player.position + Vector3.up * offsetModifier + Vector3.right * gunOffset;
                //creates bullet
                var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
                gunNoise.Play();
                //applies speed to the bullet
                bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletSpeed);
                //destroys bullet after 1 second
                Destroy(bullet, 1.0f);
            }
        }
        //shooting left
        if (Input.GetKey(KeyCode.J) && fireRateCounter <= 0 && !(Input.GetKey(KeyCode.K)) && !(Input.GetKey(KeyCode.I)))
        {
            //player faces this direction
            player.rotation = Quaternion.Euler(0f, 0f, 90f);
            //direction bullet faces
            fireDirection = Quaternion.Euler(0f, 0f, 90f);
            //set fire rate to the amount of time to wait
            fireRateCounter = fireRate;
            //offset left of player
            positionOffset = player.position + Vector3.left * offsetModifier + Vector3.up* gunOffset;
            gunNoise.Play();
            //creates bullet
            var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
            //applies speed to the bullet
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bulletSpeed);
            //destroys bullet after 1 second
            Destroy(bullet, 1.0f);
        }
        //shooting right if the right button is pressed and the bottom and top arent pressed
        if (Input.GetKey(KeyCode.L) && fireRateCounter <= 0 && !(Input.GetKey(KeyCode.K)) && !(Input.GetKey(KeyCode.I)))
        {
            //player faces this direction
            player.rotation = Quaternion.Euler(0f, 0f, -90f);
            //direction bullet faces
            fireDirection = Quaternion.Euler(0f, 0f, 90f);
            //set fire rate to the amount of time to wait
            fireRateCounter = fireRate;
            //offset to the right of player
            positionOffset = player.position + Vector3.right * offsetModifier + Vector3.down * gunOffset;
            gunNoise.Play();
            //creates bullet
            var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
            //applies speed to the bullet
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * bulletSpeed);
            //destroys bullet after 1 second
            Destroy(bullet, 1.0f);
        }
        //shooting down and bottom left and bottom right
        if (Input.GetKey(KeyCode.K) && fireRateCounter <= 0)
        {
            //shoot bottom right
            if (Input.GetKey(KeyCode.L))
            {
                //player faces this direction
                player.rotation = Quaternion.Euler(0f, 0f, -135f);
                //direction bullet faces
                fireDirection = Quaternion.Euler(0f, 0f, 45f);
                //set fire rate to the amount of time to wait
                fireRateCounter = fireRate;
                //offset so bullet doesn't spawn on player, diagonols have less offset
                positionOffset = player.position + (Vector3.down*1.2f + Vector3.right*.9f) * offsetModifier + Vector3.left * gunOffset;
                gunNoise.Play();
                //creates bullet
                var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
                //applies speed to the bullet
                bullet.GetComponent<Rigidbody2D>().AddForce((Vector2.down + Vector2.right )* bulletSpeed);
                //destroys bullet after 1 second
                Destroy(bullet, 1.0f);
            }
            //shoot bottom left
            else if (Input.GetKey(KeyCode.J)){
                //player faces this direction
                player.rotation = Quaternion.Euler(0f, 0f, 135f);
                //direction bullet faces
                fireDirection = Quaternion.Euler(0f, 0f, -45f);
                //set fire rate to the amount of time to wait
                fireRateCounter = fireRate;
                //offset so bullet doesn't spawn on player, diagonols have less offset
                positionOffset = player.position + (Vector3.down +Vector3.left ) * offsetModifier * .16f + Vector3.left * gunOffset * 1.3f;
                gunNoise.Play();
                //creates bullet
                var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
                //applies speed to the bullet
                bullet.GetComponent<Rigidbody2D>().AddForce((Vector2.down + Vector2.left) * bulletSpeed);
                //destroys bullet after 1 second
                Destroy(bullet, 1.0f);
            }
            //shoot down
            else
            {
                //player faces this direction
                player.rotation = Quaternion.Euler(0f, 0f, 180f);
                //direction bullet faces
                fireDirection = Quaternion.Euler(0f, 0f, 0f);
                //set fire rate to the amount of time to wait
                fireRateCounter = fireRate;
                gunNoise.Play();
                //offset below player
                positionOffset = player.position + Vector3.down * offsetModifier + Vector3.left * gunOffset;
                //creates bullet
                var bullet = Instantiate(bulletPrefab, positionOffset, fireDirection);
                //applies speed to the bullet
                bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.down * bulletSpeed);
                //destroys bullet after 1 second
                Destroy(bullet, .70f);
            }
            
        }

    }
    //handles walking over powerup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            if(collision.gameObject.name == "MovementSpeed(Clone)")
            {
                //if there is not an active power up, then apply the power up
                movementSpeedTimer = movementSpeedTimerDuration;
                if (!powerUpSpeed)
                {
                    playerMovement.playerSpeed *= movementSpeedModifier;
                    powerUpSpeed = true;
                    
                }
                Destroy(collision.gameObject);
            } 
            else if(collision.gameObject.name == "FireSpeed(Clone)")
            {
                gunSpeedTimer = gunSpeedTimerDuration;
                //if there is not an active power up, then apply the power up

                if (!powerUpGun)
                {
                    fireRate /= gunModifier;
                    powerUpGun = true;
                    
                }
                Destroy(collision.gameObject);
            }
        }
    }

    //accessor
    public float getGunDamage()
    {
        return gunDamage;
    }
}
