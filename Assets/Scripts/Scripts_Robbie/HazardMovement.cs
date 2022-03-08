using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardMovement : MonoBehaviour
{
    bool movingLeft;
    public float speed;

    public float distanceLimit;
    float minX;
    float maxX;

    void Start()
    {
        movingLeft = true;

        speed = speed/1000;

        minX = gameObject.transform.position.x - distanceLimit;
        maxX = gameObject.transform.position.x + distanceLimit;
    }

    void Update()
    {
        if(transform.position.x < minX) movingLeft = true;
        else if(transform.position.x > maxX) movingLeft = false;

        if(movingLeft) transform.Translate(speed, 0, 0);
        else transform.Translate(-speed, 0, 0);
    }
}
