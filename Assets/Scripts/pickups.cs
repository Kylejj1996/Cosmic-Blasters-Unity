using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Animations;
using UnityEngine;

public class pickups : MonoBehaviour
{
    //Variables
    public float minSpeed, maxSpeed;//Min and Max speed
    public float pickupSpeed;//Pickup speed
    float tempX;//Pickups X position
    float tempY;//Pickups Y position
    public float maxPositionTop, maxPositionBottom;//Top and bottom max enemy positon
    public float rightOfScreen, leftOfScreen;//Pickups position to the left of the screen and to the right of the screen
    public GameObject particle;//Particle system game object
    public player player;//Linking to the player script
    public GameObject gameManager;//Linking to the game manager object
    
    // Start is called before the first frame update
    void Start()
    {
        //Linking to the player script
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();

        //Link to the game manager object
        gameManager = GameObject.FindWithTag("GameManager").gameObject;

        //Setting a random Vertical value
        tempY = Random.Range(maxPositionTop, maxPositionBottom);

        //Setting the pickup to the right of the screen
        transform.position = new Vector2(rightOfScreen, tempY);

        //Setting a random pickup speed
        pickupSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //Move the pickup to the left of the screen
        transform.Translate((Vector3.left * pickupSpeed) * Time.deltaTime);

        //Check to see if the X position is past the left of the screen
        if (transform.position.x < leftOfScreen)
        {
            //Reset the gamemangers pickup boolean
            gameManager.GetComponent<gameManager>().PickupReset();

            //Destroy the object
            Destroy(gameObject);
        }
    }

    //Method to detect collisions
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Checking for collision to enemys bullet
        if (collision.gameObject.tag == "PlayerBullet")
        {
            if(gameObject.tag != "LoseALife")
            {
                //Create the explosion
                Instantiate(particle, transform.position, particle.transform.rotation);

                //Reset the game managers pickupOut bool
                gameManager.GetComponent<gameManager>().PickupReset();

                //Destroy the players bullet
                Destroy(collision.gameObject);

                //Destroy the pickup
                Destroy(gameObject);
            }
            else
            {
                //Create the explosion
                Instantiate(particle, transform.position, particle.transform.rotation);

                //Destroy the players bullet
                Destroy(collision.gameObject);
            }
        }

        //Checking if the collision has the tag Player
        if (collision.gameObject.tag == "Player")
        {
            player.Pickup(gameObject.tag);

            //Destroy the pickup
            Destroy(gameObject);

        }


        //Checking if the collision has the tag PlayeShield
        if (collision.gameObject.tag == "PlayerShield")
        {
            //Creating an explosion
            Instantiate(particle, transform.position, transform.rotation);

            //Destroy the enemy
            Destroy(gameObject);
        }
    }







}
