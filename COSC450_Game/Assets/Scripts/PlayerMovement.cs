using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Sets player's initial to coordintates 0,0
    private Vector2 playerLocation = new Vector2(0, 0);
    //Sets movement speed of player
    public float playerSpeed = 5f;
    //Vector direction that the player will move in
    private Vector2 movementDirection;
    //Allows for script use of rigid body component
    public Rigidbody2D rig;
    [SerializeField] SpriteRenderer sprite;
    private bool isInvincible;

    [SerializeField] GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        this.firstPosition();
        sprite = this.GetComponent<SpriteRenderer>();
        isInvincible = false;
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }
    //process the inputs in update
    void Update()
    {
        //Finds Horizontal Distance
        movementDirection.x = Input.GetAxisRaw("Horizontal");

        //Finds Vertical Distance
        movementDirection.y = Input.GetAxisRaw("Vertical");

    }

    //Process the physics in fixed update
    void FixedUpdate()
    {

        this.Move();

    }
    void firstPosition()
    {

        //Moves player to starting coordinates on start
        transform.localPosition = playerLocation;

    }

    void Move()
    {

        //Adds force to player rigid body to move
        rig.MovePosition(rig.position + movementDirection.normalized * playerSpeed * Time.fixedDeltaTime);

    }

    //called when player dies
    public void PlayerRespawn()
    {
        this.transform.position = new Vector3(0f, 0f, 0f);
        isInvincible = true;
        StartCoroutine(characterInvisible());
    }
    //flashes character sprite
    public IEnumerator characterInvisible()
    {
        sprite.color = new Color(1, 1, 1, .2f);
        yield return new WaitForSeconds(.125f);
        sprite.color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(.125f);
        sprite.color = new Color(1, 1, 1, .2f);
        yield return new WaitForSeconds(.125f);
        sprite.color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(.125f);
        sprite.color = new Color(1, 1, 1, .2f);
        yield return new WaitForSeconds(.125f);
        sprite.color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(.125f);
        sprite.color = new Color(1, 1, 1, .2f);
        yield return new WaitForSeconds(.125f);
        sprite.color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(.125f);
        sprite.color = new Color(1, 1, 1, .2f);
        yield return new WaitForSeconds(.125f);
        sprite.color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(.125f);
        sprite.color = new Color(1, 1, 1, .2f);
        yield return new WaitForSeconds(.125f);
        sprite.color = new Color(1, 1, 1, 1f);
        isInvincible = false;
    }
    //when a zombie collides with the player, remove a life
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the zombie collides with the character and the character hasnt recently died, then take down a life
        if (collision.gameObject.tag == "Enemy" && !isInvincible)
        {
            
            //call the game controller to take down a life
            gc.hasDied();
            //respawn player
            PlayerRespawn();
            

        }
    }
}

