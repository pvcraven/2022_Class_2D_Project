using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloatLevi : MonoBehaviour
{
    public float bounceRange;
    public float speed;

    Vector2 positionOffset = new Vector2();
    Vector2 positionTemp = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        positionOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        positionTemp = positionOffset;
        positionOffset.y += Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * bounceRange;
        transform.position = positionTemp;
    }
}
