using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinos : MonoBehaviour
{
    public string Name;
    public string DinoID;

    public BoxCollider2D _BoxCollider2D;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _BoxCollider2D = GetComponent<BoxCollider2D>();
    }
}
