using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onPartSystem : MonoBehaviour
{

    private ParticleSystem p_sys;

    // Start is called before the first frame update
    void Start()
    {
        p_sys = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (p_sys)
        {
            if (!p_sys.isPlaying)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
