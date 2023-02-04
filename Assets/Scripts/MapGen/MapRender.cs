using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MapRender : MonoBehaviour
{
    public GameObject map_handler;
    public GameObject block_prefab;
    
    public int[,] world_map;
    public int block_size = 16;

    private void Awake()
    {
        var handler = map_handler.GetComponent<MapHandler>();
        world_map = handler.GenerateMap();
        
        Debug.Log(world_map.GetLength(0));
        Debug.Log(world_map.GetLength(1));

        string debug_matrix = " ";
        
        for (int y = 0; y < world_map.GetLength(1); y++)
        {
            for (int x = 0; x < world_map.GetLength(0); x++)
            {
                string tile = "[" + world_map[x, y] + "]";
                debug_matrix += tile;
            }
        }
        
        Debug.Log(debug_matrix);
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
                        placed_block.GetComponent<MeshRenderer>().material.color = Color.cyan;
                        break;
                    
                    case 2:
                        placed_block.GetComponent<MeshRenderer>().material.color = Color.green;
                        break;
                    
                    case 3:
                        placed_block.GetComponent<MeshRenderer>().material.color = Color.red;
                        break;
                    
                    case 4:
                        placed_block.GetComponent<MeshRenderer>().material.color = Color.magenta;
                        break;
                    
                    default:
                        placed_block.GetComponent<MeshRenderer>().material.color = Color.black;
                        break;
                }
            }
        }
    }
}
