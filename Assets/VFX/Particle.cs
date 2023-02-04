using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using System.Collections;
using System.Collections.Generic;

public class Particle : MonoBehaviour
{

    public ParticleSystem sparkle;
    public ParticleSystem hit_ground;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            Instantiate(sparkle, this.transform);
        }
    }
}
