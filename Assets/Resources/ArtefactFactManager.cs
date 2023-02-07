using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
//using UnityEditor;
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
    public GameObject DinoPrefab;
    
    private Dictionary<string, string> descriptions = new Dictionary<string, string>();
    private Dictionary<string, string[]> layers = new Dictionary<string, string[]>();
    
    [SerializeField] public TextAsset descriptions_json_file;
    [SerializeField] public TextAsset layers_json_file;
    [SerializeField] private string[] asset_folder_path = {"Assets/Prefabs/BLOCKPREFABS"};

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

    public string getDescription(string name)
    {
        return descriptions[name];
    }

    public Dictionary<string, string[]> getLayers()
    {
        return layers;
    }

    public Dictionary<int, List<KeyValuePair<string, GameObject>>> getBlockPrefabs()
    {
        // int = layer // string = name // GameObject = prefab.
        Dictionary<int, List<KeyValuePair<string, GameObject>>> prefabs = new Dictionary<int, List<KeyValuePair<string, GameObject>>>();

        int layer_count = 0;
        
        foreach (var layer in layers)
        {
            List<KeyValuePair<string, GameObject>> layer_prefabs = new List<KeyValuePair<string, GameObject>>();
            layer_prefabs.Add(new KeyValuePair<string, GameObject>(layer.Key, DinoPrefab));
            
            foreach (var bone in layer.Value)
            {
                //string[] identifiers = AssetDatabase.FindAssets("DinoBase", asset_folder_path);
                layer_prefabs.Add(new KeyValuePair<string, GameObject>(bone, DinoPrefab));
                
                // for (int i = 0; i < identifiers.Length; i++) 
                // {
                //     string path = AssetDatabase.GUIDToAssetPath(identifiers[i]);
                //     layer_prefabs.Add(new KeyValuePair<string, GameObject>(bone,AssetDatabase.LoadAssetAtPath<GameObject>(path)));
                // }
            }
            
            //layer_prefabs.Add(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.FindAssets("Block", asset_folder_path)[0]));
            prefabs.Add(layer_count, layer_prefabs);
            layer_count++;
        }

        return prefabs;
    }
}
