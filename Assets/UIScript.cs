using System.Collections;
using System.Collections.Generic;
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
    // ---------- //
    void Start()
    {
        depthslider = gameObject.transform.Find("Depth_slider").GetComponent<Slider>();
        depthslider.value = 1f; //starts the player at the top of the depth slider.
        
        picklogo = gameObject.transform.Find("Pick_level").GetComponent<Image>();
        shovelogo = gameObject.transform.Find("Shovel_level").GetComponent<Image>();
    }
    
    void FixedUpdate()
    {
        pheight = Mathf.Abs(player.transform.position.y / gameheight);
        depthslider.value = 1f - pheight;
    }

    void NewTools()
    {
        //call this when the player's tools get upgraded
        picklogo.sprite = Resources.Load<Sprite>("");
        shovelogo.sprite = Resources.Load<Sprite>("");
    }
}
