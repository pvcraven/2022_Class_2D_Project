using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miguelBullet : MonoBehaviour
{
    Vector3 _origin;
    public float maxDistance = 8.0f;
    public GameObject burstPrefab;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        // Get position we started at, so we can see how far the bullet traveled.
        _origin = transform.position;
        body = GetComponent<Rigidbody2D>();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.tag == "Destroyable")
        {

            Debug.Log("Destroyable");
            // Destroy item we hit
            Destroy(collision.gameObject);
            // Cause bullet to destroy itself
            // Put this outside the if to get deleted when hitting non-destroyable objects
            Destroy(gameObject);
            var burst = Instantiate(burstPrefab, body.position, Quaternion.identity);
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