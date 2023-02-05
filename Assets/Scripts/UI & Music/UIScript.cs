using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject player;
    // DEPTH SLIDER //
    private Slider depthslider;
    private float pheight;
    private float gameheight = 10f; //can just be a magic number for now.
    
    // ----------- //
    
    //TOOL LEVELS //
    private Image picklogo;
    private Image shovelogo;
    private int picknum = 1;
    private int shovelnum = 4;
    private Sprite[] tools;
    // ---------- //
    void Start()
    {

        depthslider = gameObject.transform.Find("Depth_slider").GetComponent<Slider>();
        depthslider.value = 1f; //starts the player at the top of the depth slider.
        
        picklogo = gameObject.transform.Find("Pick-level").GetComponent<Image>();
        shovelogo = gameObject.transform.Find("Shovel-level").GetComponent<Image>();
        tools = Resources.LoadAll<Sprite>("Sprites/tools");
    }
    
    void FixedUpdate()
    {
        if (player.transform.position.y <= 0) 
        {
            pheight = Mathf.Abs(player.transform.position.y / gameheight);
            depthslider.value = 1f - pheight;
        }
    }

    public void NewTools(bool pick = false, bool shovel = false)
    {
        //call this when the player's tools get upgraded
        if (pick && picknum < 3)
        {
            picknum += 1;
            picklogo.sprite = tools[picknum];
        }
        if (shovel && shovelnum < 6)
        {
            shovelnum += 1;
            shovelogo.sprite = tools[shovelnum];
        }
    }
}
