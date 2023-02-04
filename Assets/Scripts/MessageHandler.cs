using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{

    [SerializeField]private GameObject _messagePrefab; // Reference to the message prefab
    private Animator _animator;
    private GameObject _canvas;
    private bool _canvasFound = false;

    // Message header
    private GameObject _headerGameObject;
    private TextMeshProUGUI _headerText;
    
    // Message description
    private GameObject _descriptionGameObject;
    private TextMeshProUGUI _descriptionText;
    
    private void Awake()
    {
        _headerGameObject = GameObject.Find("Header"); // Find header GameObject
        _descriptionGameObject = GameObject.Find("Description"); // Find description GameObject
    }

    private void Start()
    {
        _headerText = _headerGameObject.GetComponent<TextMeshProUGUI>();
        _descriptionText = _descriptionGameObject.GetComponent<TextMeshProUGUI>(); 
        _animator = GameObject.FindGameObjectWithTag("Message").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     _animator = GameObject.FindGameObjectWithTag("Message").GetComponent<Animator>();
        //     _animator.SetBool("ShowMessage", true);
        // }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UpdateMessageContent("T-Rex", "This is the description");
        }

        if (!GameObject.FindGameObjectWithTag("Canvas") && !_canvasFound) return;
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        _canvasFound = true;
    }

    public void UpdateMessageContent(string header, string description)
    {
        StartCoroutine(UpdateMessageCoroutine(header, description));
    }


    IEnumerator UpdateMessageCoroutine(string header, string description)
    {
        _animator.SetBool("ShowMessage", false);
        yield return new WaitForSeconds(1.2f);
        _headerText.text = header;
        _descriptionText.text = description;
        _animator.SetBool("ShowMessage", true);
    }
}
