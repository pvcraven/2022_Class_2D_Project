using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EanParticleScript : MonoBehaviour
{
    private int frame;
    private int lifetime = 2;
    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frame = frame + 1;
        if (frame >= lifetime * 60)
        {
            Destroy(gameObject);
        }
    }
}
