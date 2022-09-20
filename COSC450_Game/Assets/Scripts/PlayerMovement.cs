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

    // Start is called before the first frame update
    void Start()
    {
        this.firstPosition();
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

}

