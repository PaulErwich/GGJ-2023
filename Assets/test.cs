using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
        //var main = ps.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
