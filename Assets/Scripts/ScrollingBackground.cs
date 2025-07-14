using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    //Variables
    public float scrollSpeed;//Background speed

    public float tempX;//X Position of background

    public float width;//Width of the object

    public SpriteRenderer myRenderer;//Objects Sprite Renderer


    //Awake is called before start
    private void Awake()
    {
        //Set the temp position to game object current X position
        tempX = transform.position.x;

        //The sprite renderer of this object
        myRenderer = GetComponent<SpriteRenderer>();

        //Set the width of this object
        width = myRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Set the new X position
        tempX -= scrollSpeed * Time.deltaTime;

        //Setting the new backgrounds X position and keeping Y the same
        transform.position = new Vector2(tempX, transform.position.y);  

        if(transform.position.x < -width)
        {
            //Positions the background directly to the right of the current visible object
            Vector2 groundOffSet = new Vector2(width * 2f, 0);

            //Move the object for the offscreen position to the right of the screen
            transform.position = (Vector2)transform.position + groundOffSet;

            //Set the tempX to the X position of the background
            tempX = transform.position.x;
        }
    }
}
