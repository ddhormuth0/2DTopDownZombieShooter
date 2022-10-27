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

    Vector2 moveDirection;

    private void Awake()
    {
        zombie = this.GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        gcScript = GameObject.Find("GameController").GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
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
