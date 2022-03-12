using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public string dialoguePath;
    public string alternatePath;
    public Color color;
    public AudioSource dialogueSound;
    public float lowPitch;
    public float highPitch;

    public bool interactedOnce;

    void Start()
    {
        interactedOnce = false;
    }

    public IEnumerator firstInteraction()
    {
        //sets interacted once to true. 
        //Needs to be delayed in order for other coroutines to end.
        yield return new WaitForSeconds(.1f);
        interactedOnce = true;
        StopCoroutine(firstInteraction());
    }
}
