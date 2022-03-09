using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public float min = 2f;
    public float max = 2f;

    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.x - 4;
        max = transform.position.x + 4;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
    }
}
