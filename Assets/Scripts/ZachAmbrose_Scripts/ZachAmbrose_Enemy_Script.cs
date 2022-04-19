using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZachAmbrose_Enemy_Script : MonoBehaviour
{
    // Game object that will be moving
    //public GameObject game_object;

    // Generic varibales to move the enemy between two points (including diagonally)
    public int move_speed = 2;
    public float left_x = 0;
    public float right_x = 0;
    public float top_y = 0;
    public float bottom_y = 0;

    public int health = 6;

    public AudioClip enemyDeathSound;


    private SpriteRenderer spriteRenderer;
    private Animator animator;

    float move_horizontal = 0;
    float move_vertical = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(enemyDeathSound, transform.position);
        }
    }

    void FixedUpdate()
    {

        Vector3 movement = new Vector3(move_horizontal, move_vertical);
        GetComponent<Rigidbody2D>().velocity = movement * move_speed;

        // Checking x values (to not move x axis make each = "spawn" position)
        if (transform.position.x < left_x)
        {
            move_horizontal = 1;
        }
        if (transform.position.x > right_x)
        {
            move_horizontal = -1;
        }
        
        // Checking y values (to not move y axis make each = "spawn" position)
        if (transform.position.y > top_y)
        {
            move_vertical = -1;
        }
        if (transform.position.y < bottom_y)
        {
            move_vertical = 1;
        }

        if (move_horizontal > 0.1)
            spriteRenderer.flipX = false;

        else if (move_horizontal < -0.1)
            spriteRenderer.flipX = true;

    }

}
