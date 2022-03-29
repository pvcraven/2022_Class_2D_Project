using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peter_Enemy_Script : MonoBehaviour
{
    // Game object that will be moving
    //public GameObject game_object;

    // Generic varibales to move the enemy between two points (including diagonally)
    public int move_speed = 2;
    public float left_x = 0;
    public float right_x = 0;
    public float top_y = 0;
    public float bottom_y = 0;

    int move_horizontal = 0;
    int move_vertical = 0;

    public Animator enemyAnimator;

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
    }

    void IdleMoment()
    {
        enemyAnimator.SetBool("Moving", false);
        move_horizontal = 0;
        move_vertical = 0;
        enemyAnimator.SetBool("Moving", true);
    }

}
