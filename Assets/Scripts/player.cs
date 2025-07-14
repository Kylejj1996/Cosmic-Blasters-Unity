using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{

    //Variables
    public GameObject selectedObject;//The player

    Vector3 offset;//Var tracking offset between the clicked player and mouse/click input

    float tempX;//The players X position

    float tempY;//The players Y position

    public float maxMoveTop, maxMoveBottom;//Top and Bottom player Movements

    public float maxMoveLeft, maxMoveRight;//Left and Right Player Movements

    public float fireRate;//Firerate speed

    float originalFireRate;//The original fire rate

    public float fireTime = 0.0f;//Counts the time between players time to fire

    public GameObject playerBullets;//Players bullet prefab

    public Transform fire1, fire2, fire3, fire4, fire5, fire6;//Firepoints

    public int firePower = 1;//Fire power

    public int playerScore = 0;//Players score

    public Text scoreText;//Text-Score

    public int playerLives = 3;//Players Lives

    public Text livesText;//Text-Lives

    public string activePickup;//Track which pickup is active

    public GameObject gameManager;//Link to the game manager

    public GameObject shield;//The players shield

    // Start is called before the first frame update
    void Start()
    {
        //Setting the score text
        scoreText.text = "Score: " + playerScore;

        //Setting the lives text
        livesText.text = "Lives: " + playerLives;

        //Linking to the players fire points
        fire1 = transform.Find("Firepoint1").gameObject.transform;
        fire2 = transform.Find("Firepoint2").gameObject.transform;
        fire3 = transform.Find("Firepoint3").gameObject.transform;
        fire4 = transform.Find("Firepoint4").gameObject.transform;
        fire5 = transform.Find("Firepoint5").gameObject.transform;
        fire6 = transform.Find("Firepoint6").gameObject.transform;

        //Saving the fire rate to be reset later
        originalFireRate = fireRate;

        //Load the shield
        shield = transform.Find("shield").gameObject;

        //Linking to the game Manager
        gameManager = GameObject.FindWithTag("GameManager").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //The mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //If there is a mouse/touch input
        if (Input.GetMouseButtonDown(0))
        {
            //Does mouse/touch position overlap a collider
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            //If targetObject is not null - empty - we check for the player being clicked
            if (targetObject != null)
            {
                //If the collider belongs to the player
                if (targetObject.tag == "Player")
                {
                    //Set selected object to the player
                    selectedObject = targetObject.transform.gameObject;

                    //Set the offset between the mouse/touch position and player
                    offset = selectedObject.transform.position - mousePosition;
                }
            }
        }

        //If there is a selected player 
        if (selectedObject)
        {
            //Set the player position equals the mouse position + the offset value
            selectedObject.transform.position = mousePosition + offset;

            //Get the new X position for player based off input
            tempX = selectedObject.transform.position.x;

            //Get the new Y position for player based off input
            tempY = selectedObject.transform.position.y;

            //If statement to check if the tempX value is greater than the max left value on screen
            if (tempX < maxMoveLeft)
            {
                //Set the tempX value to the max left value
                tempX = maxMoveLeft;
            }

            //If statement to check if the tempX value is greater than the max right value on screen
            if (tempX > maxMoveRight)
            {
                //Set the tempX value to the max right value
                tempX = maxMoveRight;
            }

            //If statement to check if the tempX value is greater than the max top value on screen
            if (tempY > maxMoveTop)
            {
                //Set the tempX value to the max top value
                tempY = maxMoveTop;
            }

            //If statement to check if the tempX value is greater than the max bottom value on screen
            if (tempY < maxMoveBottom)
            {
                //Set the tempX value to the max bottom value
                tempY = maxMoveBottom;
            }

            //Set the players new X position
            transform.position = new Vector2(tempX, tempY);
        }

        //If the mouse button is up and there was a selected object
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            //Set the selectedObject to null - no selectedObject
            selectedObject = null;
        }

        //If it's time for the player to fire
        if (Time.time >= fireTime)
        {
            //reset firetime
            fireTime = Time.time + fireRate;

            //if the player is at normal firepower
            if (firePower == 1)
            {
                //Creating new bullets at the players location give it the players position and rotation
                Instantiate(playerBullets, fire1.transform.position, fire1.transform.rotation);
                Instantiate(playerBullets, fire2.transform.position, fire2.transform.rotation);
            }

            //if the player has fire power 2
            if (firePower == 2)
            {
                //Creating a new bullet at the players location 
                Instantiate(playerBullets, fire1.transform.position, fire1.transform.rotation);

                //Creating a new bullet at the players location
                Instantiate(playerBullets, fire2.transform.position, fire2.transform.rotation);

                //Creating a new bullet at the players location 
                Instantiate(playerBullets, fire3.transform.position, fire3.transform.rotation);

                //Creating a new bullet at the players location 
                Instantiate(playerBullets, fire4.transform.position, fire4.transform.rotation);
            }

            //if the player has fire power 3
            if (firePower == 3)
            {
                //Creating a new bullet at the players location 
                Instantiate(playerBullets, fire1.transform.position, fire1.transform.rotation);

                //Creating a new bullet at the players location 
                Instantiate(playerBullets, fire2.transform.position, fire2.transform.rotation);

                //Creating a new bullet at the players location 
                Instantiate(playerBullets, fire3.transform.position, fire3.transform.rotation);

                //Creating a new bullet at the players location 
                Instantiate(playerBullets, fire4.transform.position, fire4.transform.rotation);

                //Creating a new bullet at the players location give 
                Instantiate(playerBullets, fire5.transform.position, fire5.transform.rotation);

                //Creating a new bullet at the players location give 
                Instantiate(playerBullets, fire6.transform.position, fire6.transform.rotation);
            }
        }

        //Checking to see if player lives is less than or equal to zero
        if (playerLives <= 0)
        {
            //Updating the final score
            gameManager.GetComponent<gameManager>().finalScore = playerScore;

            //From the game
            gameManager.GetComponent<gameManager>().fromGame = true;

            //Load the scores scene
            SceneManager.LoadScene("scene5_HighScores");
        }




    }

    //Method to add points to the player score
    public void AddScore(int points)
    {
        //Adding to the player score
        playerScore += points;

        //Updatind the score text
        scoreText.text = "Score: " + playerScore;
    }

    public void subtractLife()
    {
        //Subtracting a life from the player
        playerLives -= 1;

        //Updating the lives text
        livesText.text = "Lives: " + playerLives;
    }

    public void Pickup(string pickupName)
    {
        //Pass the tag into the activePickup variable
        activePickup = pickupName;

        //For firePower2 Pickup
        if (pickupName == "FirePower2")
        {
            //The level of firepower
            firePower = 2;

            //Increasing the firerate by 2
            fireRate /= 2.0f;

            //Tell the gamemanager that the player got a pickup
            gameManager.GetComponent<gameManager>().PickupCaptured();
        }

        //For firePower3 Pickup
        if (pickupName == "FirePower3")
        {
            //The level of firepower
            firePower = 3;

            //Increasing the firerate by 3
            fireRate /= 3.0f;

            //Tell the gamemanager that the player got a pickup
            gameManager.GetComponent<gameManager>().PickupCaptured();
        }

        //For lose a life Pickup
        if (pickupName == "LoseALife")
        {
            //Call the method to Subtract a life from the player
            subtractLife();

            //Tell the gamemanager to reset the pickup
            gameManager.GetComponent<gameManager>().PickupReset();
        }

        //For shield Pickup
        if (pickupName == "Shield")
        {
            //Activate the players shield
            shield.SetActive(true);

            //Tell the gamemanager that the player got a pickup
            gameManager.GetComponent<gameManager>().PickupCaptured();
        }

        //For powerbomb Pickup
        if (pickupName == "SmartBomb")
        {
            //Create an array of enemies
            GameObject[] Enemys;

            //Find all game objects tagged enemy
            Enemys = GameObject.FindGameObjectsWithTag("EnemyBig");

            //Go through each of these objects
            foreach (GameObject Enemy in Enemys)
            {
                //Call the method to detroy all of the enemies
                Enemy.GetComponent<enemy>().Death();
            }

            //Tell the gamemanager to reset the pickup
            gameManager.GetComponent<gameManager>().PickupReset();
        }
    }

    //Method to turn off the players pickup
    public void TurnOffPickup()
    {
        //The firepower pickup
        if (activePickup == "FirePower2" || activePickup == "FirePower3")
        {
            //Set the firepower back to normal
            firePower = 1;

            //Set the firerate back to normal
            fireRate = originalFireRate;
        }

        //The shield pickup
        if (activePickup == "Shield")
        {
            //Turning off the shield
            shield.SetActive(false);
        }
    }
}
