using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource audio;
    private bool began;
    private int layernum;

    private AudioClip[] songs;
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        songs = Resources.LoadAll<AudioClip>("Audio/Songs");
        Time.timeScale = 0;
        began = false;
    }
    void Update()
    {
        if (Input.anyKey && began == false)
        {
            layernum = 1;
            audio.clip = songs[layernum];
            audio.Play();
            Time.timeScale = 1;
            gameObject.transform.Find("Title").gameObject.SetActive(false);
            began = true;
        }
    }

    void Deeper()
    {
        layernum += 1;
        audio.clip = songs[layernum];
        audio.Play();
    }

    void Higher()
    {
        layernum -= 1;
        audio.clip = songs[layernum];
        audio.Play();
    }
}
