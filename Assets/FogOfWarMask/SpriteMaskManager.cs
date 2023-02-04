using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteMaskManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] allSprites;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private bool maskEnabled;

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
        
        mainCamera.backgroundColor = new Color(0.1921569f,0.3019608f,0.4745098f,1);
    }
}
