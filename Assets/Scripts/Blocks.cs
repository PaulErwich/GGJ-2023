using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Blocks : MonoBehaviour
{
    public GameObject Grid;
    public Tilemap Tilemaps;
    public TileBase TileBase;
    public int ID;
    

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D[] _boxCollider2D;
    [SerializeField]private GameObject _blockPrefab;

    private void Awake()
    {
        Grid = GameObject.Find("Grid");
        Tilemaps.transform.SetParent(Grid.transform);
    }

    public void PlaceBlock(Vector3Int pos)
    {
        Tilemaps.SetTile(pos, TileBase);
    }
}
