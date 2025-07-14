using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //Variables
    public float minSpeed, maxSpeed;//Min and Max spee
    public float enemySpeed;//Enemy speed
    float tempX;//Enemys X position
    float tempY;//Enemys Y position
    public float maxPositionTop, maxPositionBottom;//Top and bottome max enemy positon
    public float rightOfScreen, leftOfScreen;//Enemies position to the left of the screen and to the right of the screen
    public GameObject particle;//Particle system game object
    public player player;//Linking to the player script
    public int enemyValue;//Number of points enemy is worth
    public int hitPoints;//Enemy hit points
    public SpriteRenderer myNumber;//Enemys number sprite

    //Link to all number sprites
    public Sprite number1, number2, number3, number4, number5, number6;
    void ResetEnemy()
    {
        //Set the enemys X position to the right of the screen
        tempX = rightOfScreen;

        //Give the enemy a random Y position 
        tempY = Random.Range(maxPositionTop, maxPositionBottom);

        //Setting the enemy speed
        enemySpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setting the variables for enemys X, Y and speed
        ResetEnemy();

        //A random number 0 - 3
        int tempSize = Random.Range(0, 3);

        //Linking to the enemys sprite Renderer
        myNumber = transform.Find("numbers").GetComponent<SpriteRenderer>();

        //Check the random number and set scale and hit points based on results
        switch (tempSize)
        {
            case 0:

                //Rescale enemy
                transform.localScale = new Vector3(.75f, .75f, 1);

                //Enemys hit points
                hitPoints = 2;

                //Display the enemys hit points
                myNumber.sprite = number2;

                //Set the enemys value
                enemyValue = 50;

                break;

            case 1:

                //Rescale enemy
                transform.localScale = new Vector3(1, 1, 1);

                //Enemys hit points
                hitPoints = 4;

                //Display the enemys hit points
                myNumber.sprite = number4;

                //Set the enemys value
                enemyValue = 75;

                break;

            case 2:
                //Rescale enemy
                transform.localScale = new Vector3(1.25f, 1.25f, 1);

                //Enemys hit points
                hitPoints = 6;

                //Display the enemys hit points
                myNumber.sprite = number6;

                //Set the enemys value
                enemyValue = 100;

                break;

        }

        //Setting the enemys new X and Y position
        transform.position = new Vector2(tempX, tempY);

        //Linking to the player script
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set the new X position
        tempX -= enemySpeed * Time.deltaTime;

        //Check to see if the enemys X past the left of the screen
        if (transform.position.x < leftOfScreen)
        {
            //Calling the method to reset the enemy
            ResetEnemy();
        }

        //Setting the enemys new X and Y position
        transform.position = new Vector2(tempX, tempY);
    }

    //Method to detect collision
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Checking for collision to enemys bullet
        if (collision.gameObject.tag == "PlayerBullet")
        {
            //Destroy the players bullet
            Destroy(collision.gameObject);

            //Create an explosion prefab at the enemy position and rotation
            Instantiate(particle, transform.position, particle.transform.rotation);

            //Remove hit points
            hitPoints--;

            //Change number graphic based on new hit points
            switch (hitPoints)
            {
                case 5:
                    //Display the enemys hit points
                    myNumber.sprite = number5;
                    break;
                case 4:
                    //Display the enemys hit points
                    myNumber.sprite = number4;
                    break;
                case 3:
                    //Display the enemys hit points
                    myNumber.sprite = number3;
                    break;
                case 2:
                    //Display the enemys hit points
                    myNumber.sprite = number2;
                    break;
                case 1:
                    //Display the enemys hit points
                    myNumber.sprite = number1;
                    break;
            }

            //Check if the enemy is dead
            if (hitPoints <= 0)
            {
                //Adding points to the player
                player.AddScore(enemyValue);

                //Destroy the enemy
                Destroy(gameObject);
            }

        }

        //Checking if the collision has the tag Player
        if (collision.gameObject.tag == "Player")
        {
            //Subtracting a life from the player
            player.subtractLife();

            //Creating a explosion 
            Instantiate(particle, transform.position, transform.rotation);

            //Destroy the enemy
            Destroy(gameObject);

        }


        //Checking if the collision has the tag PlayeShield
        if (collision.gameObject.tag == "Shield")
        {
            //Creating an explosion
            Instantiate(particle, transform.position, transform.rotation);

            //Destroy the enemy
            Destroy(gameObject);
        }
    }

    //Called when the player activates the Smart Bomb
    public void Death()
    {
        //Adding points to the player
        player.AddScore(enemyValue);

        //Destroy the enemy
        Destroy(gameObject);
    }
}
