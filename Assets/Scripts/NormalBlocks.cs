using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlocks : MonoBehaviour
{
    public string Name;
    public int BlockID = 0;
    public Sprite[] Sprite;

    public BoxCollider2D _BoxCollider2D;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {        
        _spriteRenderer.sprite = BlockID > 0 ? Sprite[1] : Sprite[0];
        Name = BlockID > 0 ? "TopBlock" : "EarthBlock";
    }
}
