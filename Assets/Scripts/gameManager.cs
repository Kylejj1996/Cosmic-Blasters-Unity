using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class gameManager : MonoBehaviour
{
    //Variables
    public int currentLevel = 1;//Setting the game level
    public GameObject enemy;//Linking to the enemy prefab
    public Text levelText;//Linking to the level text
    public bool noEnemies = true;//Boolean for no enemies
    public float timer = 0.0f;//Timer for the text
    public float textTime = 3.0f;//Timer for time the level text is on screen
    public string finalLevel;//For the final level to pass the high score screen

    public List<GameObject> pickups = new List<GameObject>();//List of the pickups
    public float releaseTimer = 0.0f;//Timer to track time between drops
    public float minReleaseTime, maxReleaseTime;//Min and Max pickup release time
    public float pickupTime;//To track next time to drop the pickup
    public bool pickupOut = false;//To tack if a pickup is currently out


    public bool pickupActive = false;//Bool to track if the pickup is active
    float pickupTimer = 0.0f;//Pickup active timer 
    public GameObject pickupPowerBar;//Linking to the pickup timer powerbar
    public GameObject player;//Linking to the player GO

    public bool fromGame = false;//Am i coming from the game scene
    //The high scores objects
    public Text highScore_1, highScore_2, highScore_3, highScore_4, highScore_5, highScore_6, highScore_7, highScore_8, highScore_9, highScore_10;

    //The level text objects on the high score page
    public Text level_1, level_2, level_3, level_4, level_5, level_6, level_7, level_8, level_9, level_10;

    public bool checkForNewHighScore = false;//Checking for the new high score
    public int finalScore;//Holds the final score
    public int highlightText = 0;//Hold the text to highlight if the player gets a high score





    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        //Set the pickup drop time
        pickupTime = Random.Range(minReleaseTime, maxReleaseTime);

        //Checking if high score 1 exists
        if (PlayerPrefs.HasKey("HighScore_1"))
        {
            //Telling the console it goes exist
            Debug.Log("The Key " + "HighScore_1" + " Exists");
        }
        else
        {
            //Telling the console it does not exist
            Debug.Log("The Key " + "HighScore_1" + " does not exist");

            //The integer key with a value of zero
            PlayerPrefs.SetInt("HighScore_1", 0);

            //Create a key with a starting value of no level
            PlayerPrefs.SetString("GameLevel_1", "no-level");

            //The integer key with a value of zero
            PlayerPrefs.SetInt("HighScore_2", 0);

            //Create a key with a starting value of no level
            PlayerPrefs.SetString("GameLevel_2", "no-level");

            //The integer key with a value of zero
            PlayerPrefs.SetInt("HighScore_3", 0);

            //Create a key with a starting value of no level
            PlayerPrefs.SetString("GameLevel_3", "no-level");

            //The integer key with a value of zero
            PlayerPrefs.SetInt("HighScore_4", 0);

            //Create a key with a starting value of no level
            PlayerPrefs.SetString("GameLevel_4", "no-level");

            //The integer key with a value of zero
            PlayerPrefs.SetInt("HighScore_5", 0);

            //Create a key with a starting value of no level
            PlayerPrefs.SetString("GameLevel_5", "no-level");

            //The integer key with a value of zero
            PlayerPrefs.SetInt("HighScore_6", 0);

            //Create a key with a starting value of no level
            PlayerPrefs.SetString("GameLevel_6", "no-level");

            //The integer key with a value of zero
            PlayerPrefs.SetInt("HighScore_7", 0);

            //Create a key with a starting value of no level
            PlayerPrefs.SetString("GameLevel_7", "no-level");

            //The integer key with a value of zero
            PlayerPrefs.SetInt("HighScore_8", 0);

            //Create a key with a starting value of no level
            PlayerPrefs.SetString("GameLevel_8", "no-level");

            //The integer key with a value of zero
            PlayerPrefs.SetInt("HighScore_9", 0);

            //Create a key with a starting value of no level
            PlayerPrefs.SetString("GameLevel_9", "no-level");

            //The integer key with a value of zero
            PlayerPrefs.SetInt("HighScore_10", 0);

            //Create a key with a starting value of no level
            PlayerPrefs.SetString("GameLevel_10", "no-level");

            PlayerPrefs.Save();
        }










    }

    //Method to reset the pickup
    public void PickupReset()
    {
        //Set pickupOut to false
        pickupOut = false;

        //Reset the release timer
        releaseTimer = 0.0f;

        //Creating a new pickup time
        pickupTime = Random.Range(minReleaseTime, maxReleaseTime);  
    }

    //Method when the player picks up a power up
    public void PickupCaptured()
    {
        //Setting to true
        pickupActive = true;

        //No pickup is out
        pickupOut = false;

        //Reset the release timer
        releaseTimer = 0.0f;

        //Set a new random pickup time
        pickupTime = Random.Range(minReleaseTime, maxReleaseTime);

    }

    //Method to reset the game
    public void ResetGame()
    {
        //Resetting the current level
        currentLevel = 1;

        //Resetting the timer
        timer = 0.0f;

        //Resetting the pickup release timer
        releaseTimer = 0.0f;

        //Setting pickupsOut to false
        pickupOut = false;

        //Setting pickupActive to false
        pickupActive = false;

        //Setting to false
        checkForNewHighScore = false;

        //Not setting text to highlighted
        highlightText = 0;

        fromGame = false;

    }

    // Update is called once per frame
    void Update()
    {
        //If on the main menu
        if (SceneManager.GetActiveScene().name == "scene2_MainMenu")
        {
            //Resetting the game
            ResetGame();
        }
            //Checking if you are on the game scene
            if (SceneManager.GetActiveScene().name == "scene4_GamePlay")
            {
                //If the level text is empty
                if (levelText == null)
                {
                    //Grabbing the text
                    levelText = GameObject.Find("Text-Level Display").GetComponent<Text>();
                }
        

            //If there are no enemies on the screen
            if (GameObject.FindWithTag("EnemyBig") == null)
            {
                //Showing the level
                levelText.text = "Level: " + currentLevel;

                //Increasing the timer
                timer += Time.deltaTime;

                //If the timer is greater than how long the level text is on screen
                if (timer >= textTime)
                {
                    //Resetting the timer
                    timer = 0.0f;

                    //Cycling to the current level
                    for (int i = 0; i < currentLevel; i++)
                    {
                        //Creating the enemies for the level    
                        GameObject tempEnemy = Instantiate(enemy, transform.position, Quaternion.identity);

                        int temp = i * 6;

                        //Setting the sorting layer for the enemy
                        tempEnemy.GetComponent<SpriteRenderer>().sortingOrder = temp;

                        //Set the sorting order for the enemies number
                        tempEnemy.transform.Find("numbers").GetComponent<SpriteRenderer>().sortingOrder = (temp + 1);

                    }
                    //Setting the text to show on the final high score screen
                    finalLevel = "Level: " + currentLevel;

                    //Increasing the current level
                    currentLevel++;

                    //Empty the level text
                    levelText.text = "";
                }
            }

            //If there is not pickup on the screen
            if (pickupOut == false)
            {
                //Increase the release timer
                releaseTimer += Time.deltaTime;

                //If it is time to release a pickup
                if (releaseTimer >= pickupTime)
                {
                    //Set the timer to zero
                    releaseTimer = 0.0f;

                    //Generate a new random time for the drop
                    pickupTime = Random.Range(minReleaseTime, maxReleaseTime);

                    //Set the tracking bool
                    pickupOut = true;

                    //Create a new pickup
                    Instantiate(pickups[Random.Range(0, pickups.Count)]);
                }

            }


            //If the powerbar is null
            if (pickupPowerBar == null)
            {
                //Load the powerbar
                pickupPowerBar = GameObject.Find("Image - PickupPowerBar");
            }

            //If the player is null
            if (player == null)
            {
                //Load the player
                player = GameObject.Find("player");

                //Calling reset game
                ResetGame();
            }

            //If the pickup is on the screen
            if (pickupActive)
            {
                //Increase the pickup timer
                pickupTimer += Time.deltaTime;

                //Scaling the pickup powerbar down
                pickupPowerBar.transform.localScale = new Vector3(Mathf.Lerp(1.0f, 0.0f, pickupTimer / 3f), 1, 1);//SOMETHING HERE IS NOT WORKING.....

                //If the pickup bar is less than zero
                if (pickupPowerBar.transform.localScale.x <= 0.0f)
                {
                    //Setting pickup active to false
                    pickupActive = false;

                    //Reset the pickup timer
                    pickupTimer = 0.0f;

                    //Talk to the player and turn off the players pickup ability
                    player.GetComponent<player>().TurnOffPickup();
                }
            }

        }

        //If on the high score screen
        if (SceneManager.GetActiveScene().name == "scene5_HighScores")
        {
            if (highScore_1 == null)
            {
                //Getting the high scores text 
                highScore_1 = GameObject.Find("Text-Score (1)").GetComponent<Text>();
                highScore_2 = GameObject.Find("Text-Score (2)").GetComponent<Text>();
                highScore_3 = GameObject.Find("Text-Score (3)").GetComponent<Text>();
                highScore_4 = GameObject.Find("Text-Score (4)").GetComponent<Text>();
                highScore_5 = GameObject.Find("Text-Score (5)").GetComponent<Text>();
                highScore_6 = GameObject.Find("Text-Score (6)").GetComponent<Text>();
                highScore_7 = GameObject.Find("Text-Score (7)").GetComponent<Text>();
                highScore_8 = GameObject.Find("Text-Score (8)").GetComponent<Text>();
                highScore_9 = GameObject.Find("Text-Score (9)").GetComponent<Text>();
                highScore_10 = GameObject.Find("Text-Score (10)").GetComponent<Text>();
            }

            if (level_1 == null)
            {
                //Getting the level text components
                level_1 = GameObject.Find("Text-Level (1)").GetComponent<Text>();
                level_2 = GameObject.Find("Text-Level (2)").GetComponent<Text>();
                level_3 = GameObject.Find("Text-Level (3)").GetComponent<Text>();
                level_4 = GameObject.Find("Text-Level (4)").GetComponent<Text>();
                level_5 = GameObject.Find("Text-Level (5)").GetComponent<Text>();
                level_6 = GameObject.Find("Text-Level (6)").GetComponent<Text>();
                level_7 = GameObject.Find("Text-Level (7)").GetComponent<Text>();
                level_8 = GameObject.Find("Text-Level (8)").GetComponent<Text>();
                level_9 = GameObject.Find("Text-Level (9)").GetComponent<Text>();
                level_10 = GameObject.Find("Text-Level (10)").GetComponent<Text>();
            }

            //If we can from an active game
            if(fromGame == true)
            {
                //Checking to see if checked for high scores
                if (checkForNewHighScore == false)
                {
                    //Cycling through all high score Player prefs
                    for(int i = 1; i <= 10; i++)
                    {
                        //Checking to see if the players final score is greater than the current high score
                        if (finalScore > PlayerPrefs.GetInt("HighScore_" + i))
                        {
                            //Moving the scores below new high score
                            for (int x = 10; x > i; x--)
                            {
                                //Getting the high score value
                                int value = PlayerPrefs.GetInt("HighScore_" + (x - 1));

                                //Moving the high score value
                                PlayerPrefs.SetInt("HighScore_" + x, value);

                                //Setting the game level value
                                string value2 = PlayerPrefs.GetString("GameLevel_" + (x - 1));

                                //Moving the game level value
                                PlayerPrefs.SetString("GameLevel_" +  x, value2);   
                            }

                            //Writing the new high score and game level
                            PlayerPrefs.SetInt("HighScore_" + i, finalScore);
                            PlayerPrefs.SetString("GameLevel_" + i, finalLevel);

                            //Set the number of the high score to be highlighted
                            highlightText = i;

                            break;
                        }
                    }

                    //High score has been checked
                    checkForNewHighScore = true;

                    //Highlighting the new high score
                    switch (highlightText)
                    {
                        case 1:
                            highScore_1.color = Color.yellow;
                            level_1.color = Color.yellow;
                            GameObject.Find("Text-Rank (1)").GetComponent<Text>().color = Color.yellow;
                            break;
                        case 2:
                            highScore_2.color = Color.yellow;
                            level_2.color = Color.yellow;
                            GameObject.Find("Text-Rank (2)").GetComponent<Text>().color = Color.yellow;
                            break;
                        case 3:
                            highScore_3.color = Color.yellow;
                            level_3.color = Color.yellow;
                            GameObject.Find("Text-Rank (3)").GetComponent<Text>().color = Color.yellow;
                            break;
                        case 4:
                            highScore_4.color = Color.yellow;
                            level_4.color = Color.yellow;
                            GameObject.Find("Text-Rank (4)").GetComponent<Text>().color = Color.yellow;
                            break;
                        case 5:
                            highScore_5.color = Color.yellow;
                            level_5.color = Color.yellow;
                            GameObject.Find("Text-Rank (5)").GetComponent<Text>().color = Color.yellow;
                            break;
                        case 6:
                            highScore_6.color = Color.yellow;
                            level_6.color = Color.yellow;
                            GameObject.Find("Text-Rank (6)").GetComponent<Text>().color = Color.yellow;
                            break;
                        case 7:
                            highScore_7.color = Color.yellow;
                            level_7.color = Color.yellow;
                            GameObject.Find("Text-Rank (7)").GetComponent<Text>().color = Color.yellow;
                            break;
                        case 8:
                            highScore_8.color = Color.yellow;
                            level_8.color = Color.yellow;
                            GameObject.Find("Text-Rank (8)").GetComponent<Text>().color = Color.yellow;
                            break;
                        case 9:
                            highScore_9.color = Color.yellow;
                            level_9.color = Color.yellow;
                            GameObject.Find("Text-Rank (9)").GetComponent<Text>().color = Color.yellow;
                            break;
                        case 10:
                            highScore_10.color = Color.yellow;
                            level_10.color = Color.yellow;
                            GameObject.Find("Text-Rank (10)").GetComponent<Text>().color = Color.yellow;
                            break;
                        default:
                            break;
                    }

                }
            }

            //Get a display the new high score and game level for all 10 high scores
            highScore_1.text = PlayerPrefs.GetInt("HighScore_1").ToString();
            level_1.text = PlayerPrefs.GetString("GameLevel_1");

            highScore_2.text = PlayerPrefs.GetInt("HighScore_2").ToString();
            level_2.text = PlayerPrefs.GetString("GameLevel_2");

            highScore_3.text = PlayerPrefs.GetInt("HighScore_3").ToString();
            level_3.text = PlayerPrefs.GetString("GameLevel_3");

            highScore_4.text = PlayerPrefs.GetInt("HighScore_4").ToString();
            level_4.text = PlayerPrefs.GetString("GameLevel_4");

            highScore_5.text = PlayerPrefs.GetInt("HighScore_5").ToString();
            level_5.text = PlayerPrefs.GetString("GameLevel_5");

            highScore_6.text = PlayerPrefs.GetInt("HighScore_6").ToString();
            level_6.text = PlayerPrefs.GetString("GameLevel_6");

            highScore_7.text = PlayerPrefs.GetInt("HighScore_7").ToString();
            level_7.text = PlayerPrefs.GetString("GameLevel_7");

            highScore_8.text = PlayerPrefs.GetInt("HighScore_8").ToString();
            level_8.text = PlayerPrefs.GetString("GameLevel_8");

            highScore_9.text = PlayerPrefs.GetInt("HighScore_9").ToString();
            level_9.text = PlayerPrefs.GetString("GameLevel_9");

            highScore_10.text = PlayerPrefs.GetInt("HighScore_10").ToString();
            level_10.text = PlayerPrefs.GetString("GameLevel_10");

        }

    }
}
