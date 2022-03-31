using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_arrow_script : MonoBehaviour
{
    public GameObject arrowPrefab;
    public SpriteRenderer bowSprite;

    public float arrowSpeed = 80000f;

    public void SpawnArrow(string s)
    {
        // Spawning my arrow
        GameObject arrow = (GameObject)Instantiate(arrowPrefab);
        if (bowSprite.flipX)
        {
            arrow.GetComponent<SpriteRenderer>().flipX = true;
            arrow.transform.position = transform.position + new Vector3(-.6f, 0f, 0f);
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-arrowSpeed, 0f);
        }
        else
        {
            arrow.transform.position = transform.position + new Vector3(.6f, 0f, 0f);
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed, 0f);
        }

    }

}
