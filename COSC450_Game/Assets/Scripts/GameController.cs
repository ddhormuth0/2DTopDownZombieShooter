using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] int percentSpawn;
    [SerializeField] int lives;

    public Text timeText;
    public Text livesText;
    public GameObject zombiePrefab;
    public GameObject speedPowerUp;
    public GameObject fireRatePowerUp;
    public GameObject spawnArea1, spawnArea2, spawnArea3, spawnArea4;



    private void Start()
    {
        roundTimer = roundTotalTimer;
        spawnTimer = 0;
        amountToSpawn = 3;
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {

        DisplayTime(roundTimer);
        //spawn some zombies when timer is 0
        if(spawnTimer <= 0 && roundTimer > 0)
        {

            //spawn the power ups on the map, you have a 10% chance of getting one
            int powerUpPercent = Random.Range(1, 101);
            Debug.Log("Power up percent:" + powerUpPercent);
            if (powerUpPercent > percentSpawn)
            {
                //randomly chooses a power up to pick
                int powerUpNum = Random.Range(1, 3);
                //randomly choose a spot to spawn
                float randomX = Random.Range(-8.5f, 8.5f);
                float randomY = Random.Range(-4.5f, 4.5f);
                Vector3 randomPosition = new Vector3(randomX, randomY, 0f);
                switch (powerUpNum)
                {
                    //spawn speed power up
                    case 1:
                        Instantiate(speedPowerUp, randomPosition, Quaternion.Euler(0f,0f,0f));
                        return;
                    case 2:
                        Instantiate(fireRatePowerUp, randomPosition, Quaternion.Euler(0f, 0f, 0f));
                        return;
                    //error
                    default:
                        Debug.LogWarning("A power up that doesnt exist was selected");
                        return;
                }
            }

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
                amountToSpawn = Random.Range(1, 4);
            }
            //50% time left
            else if(percent >= .50f)
            {
                amountToSpawn = Random.Range(2, 5);
            }
            //25% time left
            else if (percent >= .25f)
            {
                amountToSpawn = Random.Range(4, 7);
            }
            //less than 25% time left
            else
            {
                amountToSpawn = Random.Range(6,9);

            }
            //amount of time in between spawns
            spawnTimer = Random.Range(lowerBoundSpawn, UpperBoundSpawn);
        }

        
            
    //lower timers
    if(roundTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            roundTimer -= Time.deltaTime;
        }
        //string format for lives
        livesText.text = ("Lives: " + lives);
    }
    void DisplayTime(float timeDisplay)
    {
        if (timeDisplay < 0)
        {
            timeDisplay = 0;
        }
        //converts seconds left to minutes and seconds
        float minutes = Mathf.FloorToInt(timeDisplay / 60);
        float seconds = Mathf.FloorToInt(timeDisplay % 60);
        //string format for time
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //move a players life down
    public void hasDied()
    {
        lives--;
        if(lives <= 0)
        {
            //increment the active scene to the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public int returnLives()
    {
        return this.lives;
    }
}
