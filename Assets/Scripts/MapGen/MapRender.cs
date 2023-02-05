using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MapRender : MonoBehaviour
{
    public GameObject map_handler;
    public GameObject camera_ref;
    public Blocks blocks_script;
    public GameObject block_prefab;
    public GameObject collision_block;

    public GameObject grid_ref;

    public ArtefactFactManager manager;

    // public GameObject sky_layer = null;
    // public GameObject layer1 = null;
    // public GameObject layer2 = null;
    // public GameObject layer3 = null;
    // public GameObject layer4 = null;
    // public GameObject layer5 = null;
    // public GameObject layer6 = null;
    // public GameObject layer7 = null;
    // public GameObject layer8 = null;
    // public GameObject bedrock_layer = null;
    
    public int n_of_layers = 0;

    private int[,] world_map;
    public int block_size_px = 1;
    private float block_size = 1;

    private void Awake()
    {
        blocks_script = camera_ref.GetComponent<Blocks>();
    }

    private void Start()
    {
        manager = FindObjectOfType<ArtefactFactManager>();
        
        block_size = (float)block_size_px / 100;
        
        var handler = map_handler.GetComponent<MapHandler>();
        handler.n_of_layers = n_of_layers;
        world_map = handler.GenerateMap();
        RenderMap();
    }

    public void RenderMap()
    {
        //AAAAAAA
        Dictionary<int, List<KeyValuePair<string, GameObject>>> prefabs = manager.getBlockPrefabs();

        for (int x = 0; x < world_map.GetLength(0); x++)
        {
            for (int y = 0; y < world_map.GetLength(1); y++)
            {
                Vector3Int tile_pos = new Vector3Int( (int)(-1 * x * block_size), (int)(-1 * y * block_size), 0);
                Vector3 block_pos = new Vector3( (-1.12f * x * block_size), (-1.12f * y * block_size), 0);
                
                blocks_script.PlaceBlock(tile_pos);
                
                var placed = Instantiate(collision_block, block_pos, quaternion.identity);

                int dino = 0;
                if (world_map[x, y] == 67)
                {
                    dino = UnityEngine.Random.Range(0, prefabs[0].Count);
                    
                    var GO_temp = Instantiate(prefabs[0][dino].Value, block_pos, quaternion.identity);

                    GO_temp.GetComponent<Dinos>().DinoID = prefabs[0][dino].Key;
                    
                    blocks_script.RemoveBlock(tile_pos);
                    Destroy(placed);
                }
                if (world_map[x, y] == 68)
                {
                    dino = UnityEngine.Random.Range(0, prefabs[1].Count);
                    
                    var GO_temp = Instantiate(prefabs[1][dino].Value, block_pos, quaternion.identity);

                    GO_temp.GetComponent<Dinos>().DinoID = prefabs[1][dino].Key;
                    
                    blocks_script.RemoveBlock(tile_pos);
                    Destroy(placed);
                }
                if (world_map[x, y] == 69)
                {
                    dino = UnityEngine.Random.Range(0, prefabs[2].Count);
                    
                    var GO_temp = Instantiate(prefabs[2][dino].Value, block_pos, quaternion.identity);

                    GO_temp.GetComponent<Dinos>().DinoID = prefabs[2][dino].Key;
                    
                    blocks_script.RemoveBlock(tile_pos);
                    Destroy(placed);
                }

                if (world_map[x, y] == 5)
                {
                    blocks_script.RemoveBlock(tile_pos);
                    Destroy(placed);
                }
            }
        }

        //STUPID
        grid_ref.transform.position = new Vector3(-0.56f, -0.56f, 0);
    }
}
