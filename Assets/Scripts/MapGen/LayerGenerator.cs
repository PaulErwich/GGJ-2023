using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerGenerator
{
    //Creates bones pos in an array
    public static float[,] GenerateFossils(int layer_width, int layer_height, float intensity, float sensitivity)
    {
        var fossil_location = PerlinNoiseGen.GenerateNoiseMap(layer_width, layer_height, intensity);
        
        for (int y = 0; y < layer_height; y++)
        {
            for (int x = 0; x < layer_width; x++)
            {
                if (fossil_location[x, y] > sensitivity)
                {
                    fossil_location[x, y] = 1;
                }
                else
                {
                    fossil_location[x, y] = 0;
                }
            }
        }
        
        return fossil_location;
    }

    //merges bone array in map
    public static void MergeFossils(int starting_loc, int fossil_id, float[,] fossil_location, int[,] world_map)
    {
        int section_width = fossil_location.GetLength(0);
        int section_height = fossil_location.GetLength(1);
        
        for (int y = starting_loc; y < section_height + starting_loc; y++)
        {
            for (int x = 0; x < section_width; x++)
            {
                if (fossil_location[x, y - starting_loc] > 0.5f)
                {
                    world_map[x, y] = fossil_id;
                }
            }
        }
    }

    //Creates caves and merges them in
    public static void GeneratesCaves(float intensity, int block_id, int[,] world_map, int sky_width, int bedrock_height)
    {
        int map_size_x = world_map.GetLength(0);
        int map_size_y = world_map.GetLength(1);
        
        var cave_map = PerlinNoiseGen.GenerateNoiseMap(map_size_x, map_size_y, 10.3f);
        
        for (int y = 0; y < map_size_y; y++)
        {
            for (int x = 0; x < map_size_x; x++)
            {
                if (y > sky_width && y < map_size_y - bedrock_height)
                {
                    if (cave_map[x, y] > intensity)
                    {
                        world_map[x, y] = block_id;
                    }
                }
            }
        }
    }
}
