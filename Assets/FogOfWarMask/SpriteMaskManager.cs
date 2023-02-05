using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteMaskManager : MonoBehaviour
{
    private SpriteRenderer[] allSprites;
    private Camera mainCamera;
    
    [SerializeField] private bool maskEnabled;
    [SerializeField] private SpriteRenderer playerFeather;
    [SerializeField] private SpriteRenderer cursorFeather;

    void Awake()
    { 
        allSprites = FindObjectsOfType<SpriteRenderer>();
        mainCamera = Camera.main;
        DisableSpriteMesh();
    }

    private void OnValidate()
    {
        if (maskEnabled)
        {
            EnableSpriteMesh();
        }
        else
        {
            DisableSpriteMesh();
        }
    }


    void EnableSpriteMesh()
    {
        if (playerFeather != null && cursorFeather != null)
        {
            playerFeather.enabled = true;
            cursorFeather.enabled = true;
        }
        
        foreach (SpriteRenderer render in allSprites)
        {
            render.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }

        mainCamera.backgroundColor = Color.black;
    }
    
    void DisableSpriteMesh()
    {
        foreach (SpriteRenderer render in allSprites)
        {
            render.maskInteraction = SpriteMaskInteraction.None;
        }

        if (playerFeather != null && cursorFeather != null)
        {
            playerFeather.enabled = false;
            cursorFeather.enabled = false;
        }

        //mainCamera.backgroundColor = new Color(0.1921569f,0.3019608f,0.4745098f,1);
    }
}
