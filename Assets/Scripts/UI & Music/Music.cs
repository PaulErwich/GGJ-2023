using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource audio;
    
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Begin()
    {
        audio.clip = Resources.Load<AudioClip>("Audio/Main_theme_C");
        audio.Play();
    }

    void Deeper()
    {
        audio.clip = Resources.Load<AudioClip>("Audio/Main_theme_C");
        audio.Play();
    }
}
