using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZachAmbrose_Bullet_Script : MonoBehaviour
{

    Vector3 _origin;
    public float maxDistance = 8.0f;

    public GameObject burstPrefab;
    Rigidbody2D body;

    public AudioClip enemyDeathSound;

    // Start is called before the first frame update
    void Start()
    {
        _origin = transform.position;
        body = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.tag == "Enemy")
        {

            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(enemyDeathSound, transform.position);
            Destroy(gameObject);
            var burst = Instantiate(burstPrefab, body.position, Quaternion.identity);

        }
        else if (collision.tag == "Obstacle")
        {
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
