using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioClip saw;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = saw;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()  //Plays Sound Whenever collision detected
    {
        GetComponent<AudioSource>().Play();
    }
}
