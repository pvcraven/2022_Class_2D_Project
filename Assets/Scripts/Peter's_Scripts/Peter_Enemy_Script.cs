using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peter_Enemy_Script : MonoBehaviour
{
    // Generic varibales to move the enemy between two points (including diagonally)
    public int move_speed = 2;
    public float left_x = 0;
    public float right_x = 0;
    public float top_y = 0;
    public float bottom_y = 0;

    // To keep track of Health
    public int health = 2;

    int move_horizontal = 1;
    int move_vertical = 1;

    //Switch between idle and non-idle animation
    public float waitTime = 15f;
    public float animationTime = 3f;
    private float timer = 0.0f;
    private float animationTimer = 0.0f;
    private int prev_hor;
    private int prev_ver;

    public Animator enemyAnimator;

    void FixedUpdate()
    {

        Vector3 movement = new Vector3(move_horizontal, move_vertical);
        GetComponent<Rigidbody2D>().velocity = movement * move_speed;

        // Timer to idle the enemy sometimes
        if (timer > waitTime)
        {
            if (move_horizontal != 0 || move_vertical != 0)
            {
                prev_hor = move_horizontal;
                prev_ver = move_vertical;
            }

            enemyAnimator.SetBool("Moving", false);
            move_horizontal = 0;
            move_vertical = 0;

            animationTimer += Time.deltaTime;

            if (animationTimer > animationTime)
            {
                timer = 0f;
                animationTimer = 0f;
                move_horizontal = prev_hor;
                move_vertical = prev_ver;
                enemyAnimator.SetBool("Moving", true);
            }

        }
        else
        {
            enemyAnimator.SetBool("Moving", true);
            timer += Time.deltaTime;
            // Checking x values (to not move x axis make each = "spawn" position)
            if (transform.position.x < left_x)
            {
                move_horizontal = 1;
            }
            else if (transform.position.x > right_x)
            {
                move_horizontal = -1;
            }

            // Checking y values (to not move y axis make each = "spawn" position)
            if (transform.position.y > top_y)
            {
                move_vertical = -1;
            }
            else if (transform.position.y < bottom_y)
            {
                move_vertical = 1;
            }
        }

        // Killing enemy if hp <= 0
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
