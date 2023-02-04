using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class generates a perlin noise map using a X, Y and a scale
public static class PerlinNoise
{
    public static float[,] GenerateNoiseMap(int map_width, int map_height, float scale)
    {
        float[,] noise_map = new float[map_width, map_height];

        //If scale provided is less or zero apply a base not-null scale
        if (scale <= 0)
        {
            scale = 0.001f;
        }
        
        //Generates a heath map using perlin noise
        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                float scaled_x = x / scale;
                float scaled_y = y / scale;

                noise_map[x, y] = Mathf.PerlinNoise(scaled_x, scaled_y);
            }
        }

        return noise_map;
    }
}
