using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    public int map_width = 48;
    
    public int sky_height = 16;
    public int layer_height_min = 24;
    public int layer_height_max = 32;
    public int bedrock_height = 8;

    public int n_of_layers = 3;
    public float intensity = 0.3f;
    
    //Final map data
    public int map_size_x = 0;
    public int map_size_y = 0;

    public float merge_intensity = 0.3f;
    public int merge_section = 8;
    
    private int[] layers_height;
    private int[] layers_height_start;
    private int[,] world_map;
    
    public int[,] GenerateMap()
    {
        map_size_x = map_width;
        map_size_y += sky_height;

        layers_height = new int[n_of_layers];
        layers_height_start = new int[n_of_layers];

        for (int i = 0; i < n_of_layers; i++)
        { 
            layers_height[i] = UnityEngine.Random.Range(layer_height_min, layer_height_max);
            layers_height_start[i] = map_size_y;
            map_size_y += layers_height[i];
        }

        map_size_y += bedrock_height;
        
        world_map = new int[map_size_x, map_size_y];
        
        //LAYER GEN ----------------------------------------------

        for (int y = 0; y < sky_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                world_map[x, y] = 1;
            }
        }

        for (int i = 0; i < n_of_layers; i++)
        {
            for (int y = layers_height_start[i]; y < layers_height_start[i] + layers_height[i]; y++)
            {
                for (int x = 0; x < map_width; x++)
                {
                    //CUSTOM LAYER HERE
                    world_map[x, y] = 2 + i;
                }
            }
        }

        return world_map;
    }
}

