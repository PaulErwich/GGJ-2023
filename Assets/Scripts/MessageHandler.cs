using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{

    [SerializeField]private GameObject _messagePrefab; // Reference to the message prefab
    private Animator _animator;

    private void Awake()
    {
        _animator = _messagePrefab.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            CreateMessage();
        }
    }

    public void CreateMessage()
    {
        Instantiate(_messagePrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
    }
    
}
