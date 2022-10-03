using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //total amount of time in a round and amount to spawn each round
    [SerializeField] float roundTotalTimer = 30f;
    //amount to spawn each time
    private int amountToSpawn = 1;
    //amount of time in between spawns
    float spawnTimer;
    //amount of time in a round
    private float roundTimer;
    //lower random amount for time
    [SerializeField] float lowerBoundSpawn = 0.5f;
    //higher random amount for time
    [SerializeField] float UpperBoundSpawn = 3f;

    public GameObject zombiePrefab;
    public GameObject spawnArea1, spawnArea2, spawnArea3, spawnArea4;


    private void Start()
    {
        roundTimer = roundTotalTimer;
        spawnTimer = 0;
        amountToSpawn = 3;
    }

    // Update is called once per frame
    void Update()
    {

        //spawn some zombies when timer is 0
        if(spawnTimer <= 0 && roundTimer > 0)
        {
            
            //spawn the amount of zombies specified
            for(int i = 0; i<amountToSpawn; i++)
            {
                //select a random number 1-4 and this will determine the spot spawned in
                int spot = Random.Range(1, 5);

                //spawn zombies at top
                if(spot == 1)
                {
                    //SPAWN ZOMBIE HERE
                    //if it is an even number spawn in spot 1, else spawn in spot 2
                    if(i%2 == 0)
                    {
                        Instantiate(zombiePrefab, spawnArea1.transform.position + Vector3.left*.5f, Quaternion.Euler(0, 0, 180f));
                    }
                    else
                    {
                        Instantiate(zombiePrefab, spawnArea1.transform.position + Vector3.right*.5f, Quaternion.Euler(0, 0, 180f));
                    }
                } 
                //spawn zombie at right
                else if (spot == 2)
                {
                    //SPAWN ZOMBIE HERE
                    if (i % 2 == 0)
                    {
                        Instantiate(zombiePrefab, spawnArea2.transform.position + Vector3.up * .5f, Quaternion.Euler(0, 0, -90f));
                    }
                    else
                    {
                        Instantiate(zombiePrefab, spawnArea2.transform.position + Vector3.down * .5f, Quaternion.Euler(0, 0, -90f));
                    }
                }
                //spawn zombie at bottom
                else if (spot == 3)
                {
                    if (i % 2 == 0)
                    {
                        Instantiate(zombiePrefab, spawnArea3.transform.position + Vector3.left * .5f, Quaternion.Euler(0, 0, 0f));
                    }
                    else
                    {
                        Instantiate(zombiePrefab, spawnArea3.transform.position + Vector3.right * .5f, Quaternion.Euler(0, 0, 0f));
                    }
                }
                //spawn zombie at left
                else
                {
                    //SPAWN ZOMBIE HERE
                    if (i % 2 == 0)
                    {
                        Instantiate(zombiePrefab, spawnArea4.transform.position + Vector3.up * .5f, Quaternion.Euler(0, 0, 90f));
                    }
                    else
                    {
                        Instantiate(zombiePrefab, spawnArea4.transform.position + Vector3.down * .5f, Quaternion.Euler(0, 0, 90f));
                    }
                }

            }
            //choose a random number based on time
            float percent = (roundTimer / roundTotalTimer);
            //based on 75% time left
            if(percent >= .75f)
            {
                amountToSpawn = 2;
            }
            //50% time left
            else if(percent >= .50f)
            {
                amountToSpawn = 4;
            }
            //25% time left
            else if (percent >= .25f)
            {
                amountToSpawn = 6;
            }
            //less than 25% time left
            else
            {
                amountToSpawn = 8;

            }
            //amount of time in between spawns
            spawnTimer = Random.Range(lowerBoundSpawn, UpperBoundSpawn);
        }

            
    //lower timers
    if(roundTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            roundTimer -= Time.deltaTime;
            Debug.Log(roundTimer);
        }

    }
}
