using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Packages.Rider.Editor.UnitTesting;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
     
    public static InputManager Instance
    {
        get { return instance;}
    }

    public InputAsset my_input_actions;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
         
        my_input_actions = new InputAsset();
    }

    private void OnEnable()
    {
        my_input_actions.Enable();
    }

    private void OnDisable()
    {
        my_input_actions.Disable();
    }
}

