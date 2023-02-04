using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ParticleSystem : MonoBehaviour
{

    public ParticleSystem test;

    // Start is called before the first frame update
    void Start()
    {
        test = this.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
