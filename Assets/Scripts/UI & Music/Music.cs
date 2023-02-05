using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource audio;
    private bool began;
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        Time.timeScale = 0;
        began = false;
    }
    void Update()
    {
        if (Input.anyKey && began == false)
        {
            audio.clip = Resources.Load<AudioClip>("Audio/Main_theme_C");
            audio.Play();
            Time.timeScale = 1;
            gameObject.transform.Find("Title").gameObject.SetActive(false);
            began = true;
        }
    }

    void Deeper()
    {
        audio.clip = Resources.Load<AudioClip>("Audio/Main_theme_C");
        audio.Play();
    }
}
