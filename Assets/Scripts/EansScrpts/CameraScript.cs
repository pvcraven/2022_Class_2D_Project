using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject target;
    public float xBoundary;
    public float yBoundary;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + .75f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        // transform X
        var xdifference = target.transform.position.x - transform.position.x;
        if (xdifference >= xBoundary)
        {
            var delta = xdifference - xBoundary;
            transform.position = new Vector3(transform.position.x + delta, transform.position.y, transform.position.z);
        } else if (xdifference <= -xBoundary)
        {
            var delta = xdifference + xBoundary;
            transform.position = new Vector3(transform.position.x + delta, transform.position.y, transform.position.z);
        }


        // transform y
        var ydifference = target.transform.position.y - transform.position.y;
        if (ydifference >= yBoundary)
        {
            var delta = ydifference - yBoundary;
            transform.position = new Vector3(transform.position.x, transform.position.y + delta, transform.position.z);
        }
        else if (ydifference <= -yBoundary)
        {
            var delta = ydifference + yBoundary;
            transform.position = new Vector3(transform.position.x, transform.position.y + delta, transform.position.z);
        }

    }
}
