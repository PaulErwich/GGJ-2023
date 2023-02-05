using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settingscript : MonoBehaviour
{
    private bool paused;

    private Slider volumeslider;
    private Slider audioslider;
    
    // Start is called before the first frame update
    void Start()
    {
        volumeslider = gameObject.transform.Find("Panel/Music_slider").GetComponent<Slider>();
        audioslider = gameObject.transform.Find("Panel/Sounds_slider").GetComponent<Slider>();
        volumeslider.value = 1f;
        audioslider.value = 1f;
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
            paused = true;
        }
        else if (paused)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            paused = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //volume slider * magic number = volume;
        //audio slider * magic number = volume;
    }
}
