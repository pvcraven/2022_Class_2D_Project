using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : MonoBehaviour
{
    Vector3 _origin;
    public float maxDistance = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get position we started at, so we can see how far the bullet traveled.

        _origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // How far has the bullet gone?
        float distance = Vector2.Distance(_origin, transform.position);
        // If too far, then remove ourselves from the game.
        if (distance > maxDistance)
        {
            // Cause bullet to destroy itself
            Destroy(gameObject);
        }
    }

    public void destroy(){
        Destroy(gameObject);
    }
}
