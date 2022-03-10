using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement_ZachWemhoff : MonoBehaviour
{
    public float min = 2f;
    public float max = 3f;
    public float yUp = 0.0f;
    public float yDown = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.y - yDown;
        max = transform.position.y + yUp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.z);
    }
}
