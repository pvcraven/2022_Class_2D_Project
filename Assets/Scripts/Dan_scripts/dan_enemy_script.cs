using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dan_enemy_script : MonoBehaviour
{
    public GameObject daniel_FireSlimeEnemy;
    public float speed;
    public int health;

    // Update is called once per frame
    void Update()
    {
        // Get objects current position
        Vector2 position = transform.position;
        // Calculate movement
        float newY = Mathf.Sin(Time.time * speed);
        // Set objects Y to new calculated Y
        transform.position = new Vector2(transform.position.x, newY);

        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
