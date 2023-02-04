using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    private GameObject perlin_noise;

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

    public float merge_intensity_min = 0.25f;
    public float merge_intensity_max = 0.35f;
    public int merge_section_min = 8;
    public int merge_section_max = 14;
    
    private int[] layers_height;
    private int[] layers_height_start;
    private int[,] world_map;
    
    public int[,] GenerateMap()
    {
        map_size_x = map_width;
        map_size_y += sky_height;

        layers_height = new int[n_of_layers];
        layers_height_start = new int[n_of_layers + 1];

        for (int i = 0; i < n_of_layers; i++)
        { 
            layers_height[i] = UnityEngine.Random.Range(layer_height_min, layer_height_max);
            layers_height_start[i] = map_size_y;
            map_size_y += layers_height[i];
        }

        layers_height_start[n_of_layers] = map_size_y;
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

        int j = 0;
        for (int f = 0; f < n_of_layers; f++)
        {
            j = f + 1;
            MergeLayers(layers_height_start[f], j,j+1);
        }
        MergeLayers(layers_height_start[n_of_layers], j+1,255);

        return world_map;
    }

    private void MergeLayers(int merge_start, int layer1, int layer2)
    {
        int merge_section = UnityEngine.Random.Range(merge_section_min, merge_section_max);
        float merge_intensity = UnityEngine.Random.Range(merge_intensity_min, merge_intensity_max);

        var perlin_section = PerlinNoiseGen.GenerateNoiseMap(48, merge_section, merge_intensity);
        
        float filter_y = 1 / (float)merge_section;

        for (int y = merge_start; y < merge_start + merge_section; y++)
        {
            int raw_y = y - merge_start;
            float comp_y = filter_y * raw_y;

            for (int x = 0; x < map_size_x; x++)
            {
                if (perlin_section[x, raw_y] > comp_y - 0.01)
                {
                    world_map[x, y] = layer1;
                }
                else
                {
                    world_map[x, y] = layer2;
                }
            }
        }
    }
}

