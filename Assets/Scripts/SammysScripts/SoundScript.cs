using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public static SoundScript SoundInstance;

    private void Awake()
    {
        if(SoundInstance != null && SoundInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        SoundInstance = this;
        DontDestroyOnLoad(this);
    }
}
