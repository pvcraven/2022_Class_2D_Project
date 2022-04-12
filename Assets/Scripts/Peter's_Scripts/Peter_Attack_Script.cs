using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peter_Attack_Script : MonoBehaviour
{
    // How frequently can we attack?
    public float attackTimeLimit = 0.5f;

    // Countdown timer for attacks
    private float attackCountdownTimer = 0;

    void Update()
    {
        // See if we can attack, via timer.
        if (attackCountdownTimer <= 0)
        {
            // We can attack. See if user hit space bar.
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Attack");
                attackCountdownTimer = attackTimeLimit;
            }
        }
        else
        {
            // Attack timer needs count-down
            attackCountdownTimer -= Time.deltaTime;
        }
    }
}
