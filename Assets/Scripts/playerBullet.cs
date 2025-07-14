using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    //Variables
    public float bulletSpeed;//Players bullet speed
    public float rightOfScreen;//Value for players bullet position being past the right of the screen

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.right * bulletSpeed) * Time.deltaTime);

        //Check to see if the players bullets x position past the right of the screen
        if (transform.position.x > rightOfScreen)
        {
            //Destory the players bullet as it is no longer being used
            Destroy(gameObject);
        }
    }
}
