using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    private Dictionary<string, string> descriptions;
    private Dictionary<string, List<string>> slices;

    public string json_filepath;
    private string json_string;

    private void Start()
    {
        json_string = File.ReadAllText(json_filepath);
        //JsonUtility.FromJson<>()
    }
}
