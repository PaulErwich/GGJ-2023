using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Description
{
    public string Name;
    public string Info;
}

[System.Serializable]
public class Descriptions
{
    public Description[] descriptions;
}

[System.Serializable]
public class Period
{
    public string Name;
    public string[] Dinos;
}

[System.Serializable]
public class Periods
{
    public Period[] periods;
}

public class ArtefactFactManager : MonoBehaviour
{
    private Dictionary<string, string> descriptions = new Dictionary<string, string>();
    private Dictionary<string, string[]> layers = new Dictionary<string, string[]>();
    
    [SerializeField] public TextAsset descriptions_json_file;
    [SerializeField] public TextAsset layers_json_file;

    private void Start()
    {
        Descriptions json_descriptions = JsonUtility.FromJson<Descriptions>(descriptions_json_file.text);
        foreach (Description json_description in json_descriptions.descriptions)
        {
            descriptions.Add(json_description.Name, json_description.Info);
        }

        Periods json_periods = JsonUtility.FromJson<Periods>(layers_json_file.text);
        foreach (Period json_period in json_periods.periods)
        {
            layers.Add(json_period.Name, json_period.Dinos);
        }
    }
}
