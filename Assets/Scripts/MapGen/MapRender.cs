using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MapRender : MonoBehaviour
{
    public GameObject map_handler;
    public GameObject block_prefab;

    public GameObject sky_layer = null;
    public GameObject layer1 = null;
    public GameObject layer2 = null;
    public GameObject layer3 = null;
    public GameObject layer4 = null;
    public GameObject layer5 = null;
    public GameObject layer6 = null;
    public GameObject layer7 = null;
    public GameObject layer8 = null;
    public GameObject bedrock_layer = null;
    
    public int n_of_layers = 0;

    private int[,] world_map;
    public int block_size = 1;

    private void Awake()
    {
        var handler = map_handler.GetComponent<MapHandler>();
        handler.n_of_layers = n_of_layers;
        world_map = handler.GenerateMap();
        RenderMap();
    }

    public void RenderMap()
    {
        for (int x = 0; x < world_map.GetLength(0); x++)
        {
            for (int y = 0; y < world_map.GetLength(1); y++)
            {
                Vector3 pos = new Vector3( -1 * x * block_size, -1 * y * block_size, 0);
                var rot = quaternion.identity;

                var placed_block = Instantiate(block_prefab, pos, rot);

                switch (world_map[x,y])
                {
                    case 1:
                        placed_block.GetComponent<SpriteRenderer>().material.color = Color.cyan;
                        break;
                    
                    case 2:
                        placed_block.GetComponent<SpriteRenderer>().material.color = Color.green;
                        break;
                    
                    case 3:
                        placed_block.GetComponent<SpriteRenderer>().material.color = Color.red;
                        break;
                    
                    case 4:
                        placed_block.GetComponent<SpriteRenderer>().material.color = Color.magenta;
                        break;
                    
                    default:
                        placed_block.GetComponent<SpriteRenderer>().material.color = Color.black;
                        break;
                }
            }
        }
    }
}
