using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikey : MonoBehaviour
{
    public float targetX;
    public float speed;
    float startX;
    bool goingRight = true;
    public Sprite rightSprite = null;
    public Sprite leftSprite = null;
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
        if (goingRight)
        {
            spriteRender.sprite = rightSprite;
            if (transform.position.x < targetX)
            {
                body.velocity = new Vector2(speed, 0);
            } else
            {
                goingRight = false;
            }
        } else
        {
            spriteRender.sprite = leftSprite;
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
