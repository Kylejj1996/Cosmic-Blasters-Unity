using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//For UI scenes

public class loader : MonoBehaviour
{
    //Variables

    public GameObject powerBar;//Linking to powerbar object

    float myTime = 0.0f;//The increasing loading bar

    public float powerBarMax;//Checks the powerbars width to load the next scene

    public float loadingDuration;//Time for loading screen

    // Start is called before the first frame update
    void Start()
    {
        //If the powerBar is null
        if (powerBar == null)
        {
            //Finding the powerbar in the hierarchy 
            powerBar = GameObject.Find("Image-PowerBar");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Increasing the timer
        myTime += Time.deltaTime;

        //Scaling the powerbar up from 0.0
        powerBar.transform.localScale = new Vector3(Mathf.Lerp(0.0f, powerBarMax, myTime / loadingDuration), 1, 1);

        //Check the power bars scale to see if its grater than max scale
        if (powerBar.transform.localScale.x >= powerBarMax)
        {
            //If so, load the main menu level
            SceneManager.LoadScene("scene2_MainMenu");
        }
    }
}
