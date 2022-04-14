using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miguelBullet : MonoBehaviour
{
    Vector3 _origin;
    public float maxDistance = 8.0f;
    public GameObject burstPrefab;
    // How much damage to deal
    public int damage = 2;
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
            // Get the enemy script attached to this object
            movingNPC enemyScript = collision.GetComponent<movingNPC>();

            if (enemyScript)
            {
                // Damage
                collision.GetComponent<movingNPC>().health -= damage;
                // Print health levels
                Debug.Log(collision.GetComponent<movingNPC>().health);

                // --- ToDo: destroy enemy here when health <= 0
                if (collision.GetComponent<movingNPC>().health <= 0)
                {
                    // Destroy item we hit
                    Destroy(collision.gameObject);
                    // Cause bullet to destroy itself
                    var burst = Instantiate(burstPrefab, body.position, Quaternion.identity); ;
                }
            }
            else
            {
                // We hit an enemy, but there's no script attached to it.
                Debug.Log("Enemy Script not present");
            }

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