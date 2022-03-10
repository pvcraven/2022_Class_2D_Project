using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSFX : MonoBehaviour
{

    public AudioSource sound;
    // Start is called before the first frame update
    void Start ()
    {
        Debug.Log("start debug");
        sound.GetComponent<AudioSource>();
    }

    void update ()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("If statement");
        if (collision.gameObject.tag == "Player")
        {
            sound.Play();
            Debug.Log("debug");
            Destroy(collision.gameObject);
        }
    }
}
