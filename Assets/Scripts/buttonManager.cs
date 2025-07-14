using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
    //Method to go to the main menu 
    public void MainMenu()
    {
        //Load the Main menu scene
        SceneManager.LoadScene("scene2_MainMenu");
    }
    //Method to go to the instructions 
    public void Instructions()
    {
        //Load the instructions screen scene
        SceneManager.LoadScene("scene3_Instructions");
    }
    //Method to start the game over 
    public void StartGame()
    {
        //Load the game screen scene
        SceneManager.LoadScene("scene4_GamePlay");
    }
    
    //Method to go to the high scores 
    public void HighScores()
    {
        //Load the high scores screen scene
        SceneManager.LoadScene("scene5_HighScores");
    }

    //Method to quit the game
    public void QuitGame()
    {
        //Message in the console showing that the application is quitting
        Debug.Log("Application Quit");

        //Closes the application
        Application.Quit();
    }
}
