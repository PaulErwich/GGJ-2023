using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    // DEPTH SLIDER //
    public Slider depthslider;
    private float pheight;
    private float gameheight; //can just be a magic number for now.
    // ----------- //
    
    //TOOL LEVELS //
    // ---------- //
    void Start()
    {
        depthslider.value = 1f; //starts the player at the top of the depth slider.
    }
    
    void FixedUpdate()
    {
        //get player y value, divide by total height of the game
        //pheight = player.transform.position.y / gameheight;
        depthslider.value = pheight;
    }
    
    
}
