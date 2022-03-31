using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Collision : MonoBehaviour
{
    Vector3 _origin;
    public float maxDistance = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get position we started at, so we can see how far the bullet traveled.
        _origin = transform.position;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.tag == "Enemy")
        {
            // Destroy item we hit
            Destroy(collision.gameObject);
            // Cause bullet to destroy itself
            // Put this outside the if to get deleted when hitting non-destroyable objects
            Destroy(gameObject);
        }
        else if (collision.tag == "Obstacle")
        {
            // Cause bullet to destroy itself
            // Put this outside the if to get deleted when hitting non-destroyable objects
            Destroy(gameObject);
        }
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
}
