using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Isaac : MonoBehaviour
{
    Vector3 _origin;
    public float maxDistance = 8.0f;

    public GameObject burstPrefab;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        _origin = transform.position;
        Debug.Log("Is Working");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.tag == "Destroyable")
        {
            Debug.Log("Destroyable");
            Destroy(collision.gameObject);
            var burst = Instantiate(burstPrefab, body.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(_origin, transform.position);

        if (distance > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
