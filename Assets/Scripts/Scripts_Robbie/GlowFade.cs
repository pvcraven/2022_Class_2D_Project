using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

public class GlowFade : MonoBehaviour
{
    // public Material glow;
    // public Volume PPglow;
    // bool glowing;
    // private Vignette vignette;

    // void Start()
    // {
    //     //sets up for glowfade effect
    //     glowing = false;

    //     PPglow.profile.TryGet(out vignette);
    // }

    // void Update()
    // {
    //     if (!glowing)
    //     {
    //         StartCoroutine(Glow());
    //     }
    // }

    // IEnumerator Glow()
    // {
    //     glowing = true;
    //     while(vignette.intensity.value > 2f)
    //     {
    //         yield return new WaitForSeconds(.1f);
    //         vignette.intensity.value -= .1f;
    //     }
    //     while(vignette.intensity.value < 4.3f)
    //     {
    //         yield return new WaitForSeconds(.1f);
    //         vignette.intensity.value +=.1f;
    //     }

    //     glowing = false;
    //     StopCoroutine(Glow());
    // }
}