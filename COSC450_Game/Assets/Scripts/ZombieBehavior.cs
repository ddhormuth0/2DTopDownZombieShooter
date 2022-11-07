using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    [SerializeField] float health, maxhealth = 1f;

    [SerializeField] float movementSpeed = 2f;

    Rigidbody2D zombie;
    Transform target;
    [SerializeField] GameController gcScript;

    [SerializeField] bool isBoss;
    HealthBar healthBar;

    float timeToFire;
    [SerializeField]float lowerBound;
    [SerializeField] float upperBound;

    [SerializeField] float lowerBoundCrazy;
    [SerializeField] float upperBoundCrazy;

    [SerializeField] GameObject projectile;

    [SerializeField] float offsetModifier;
    Vector3 positionOffset;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileVariance;


    Vector2 moveDirection;

    private void Awake()
    {
        zombie = this.GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        gcScript = GameObject.Find("GameController").GetComponent<GameController>();
        if (isBoss)
        {
            healthBar = this.GetComponent<HealthBar>();
            timeToFire = 1f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
        if (isBoss)
        {
            healthBar.SetMaxHealth((int)maxhealth);
        }
    }

    private void Update()
    {
        if (target)
        {
            //gives us the angle between the zombie and the player by taking the tangent of the y and x values between them
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            zombie.rotation = angle;
            moveDirection = direction;
            if (isBoss && timeToFire <= 0)
            {
                positionOffset = this.gameObject.transform.position + (direction) * offsetModifier;
                var shot = Instantiate(projectile, positionOffset, Quaternion.Euler(0f,0f,0f));
                shot.GetComponent<Rigidbody2D>().AddForce((direction + new Vector3(Random.Range(-projectileVariance, projectileVariance), Random.Range(-projectileVariance, projectileVariance),Random.Range(-projectileVariance, projectileVariance))) * projectileSpeed);
                Destroy(shot, 1.0f);
                //shoot faster
                if (health <= maxhealth * .25)
                {
                    timeToFire = Random.Range(lowerBoundCrazy, upperBoundCrazy);
                }
                else
                {
                    timeToFire = Random.Range(lowerBound, upperBound);
                }
                
            }
            if (isBoss)
            {
                timeToFire -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            zombie.velocity = moveDirection.normalized * movementSpeed;
        }
    }

    public void takeDamage(float damageDealt)
    {
        health -= damageDealt;
        if (isBoss)
        {
            healthBar.SetHealth((int)health);
        }
        //check to see if dead
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnDestroy()
    {
        gcScript.decrementZombies();
    }
}
