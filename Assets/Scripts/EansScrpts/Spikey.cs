using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikey : MonoBehaviour
{
    public float targetX;
    public float speed;
    float startX;
    bool goingRight = true;
    Rigidbody2D body;
    SpriteRenderer spriteRender;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        startX = transform.position.x;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRender.flipX = !goingRight;
        if (goingRight)
        {
            if (transform.position.x < targetX)
            {
                body.velocity = new Vector2(speed, 0);
            } else
            {
                goingRight = false;
            }
        } else
        {
            if (transform.position.x > startX)
            {
                body.velocity = new Vector2(-speed, 0);
            }
            else
            {
                goingRight = true;
            }
        }


    }
}
