using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviBulletScript : MonoBehaviour
{
    Vector3 _origin;
    public float maxDistance = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        _origin = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.tag == "Destroyable")
        {
            Debug.Log("Destroyable");
            Destroy(collision.gameObject);
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
