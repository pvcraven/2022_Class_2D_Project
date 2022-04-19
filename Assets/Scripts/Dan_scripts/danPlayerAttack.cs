using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class danPlayerAttack : MonoBehaviour
{
    // How frequently can we attack?
    public float attackTimeLimit = 0.5f;

    // Countdown timer for attacks
    private float attackCountdownTimer = 0;

    // An empty parented that says where to attack
    public Transform attackPos;
    // Radius of attack circle
    public float attackRange;
    // What layer will the enemies be on?
    public LayerMask enemyLayer;
    // How much damage to deal
    public int damage = 3;

    void Update()
    {
        // See if we can attack, via timer.
        if (attackCountdownTimer <= 0)
        {
            // We can attack. See if user hit space bar.
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Attack");
                // Reset the countdown timer
                attackCountdownTimer = attackTimeLimit;
                // What enemies did we hit?
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
                // Loop through each enemy we hit
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    // Get the enemy script attached to this object
                    dan_enemy_script enemyScript = enemiesToDamage[i].GetComponent<dan_enemy_script>();
                    // If there is an enemy script
                    if (enemyScript)
                    {
                        // Damage
                        enemiesToDamage[i].GetComponent<dan_enemy_script>().health -= damage;
                        // Print health levels
                        Debug.Log(enemiesToDamage[i].GetComponent<dan_enemy_script>().health);

                        // --- ToDo: destroy enemy here when health <= 0
                    }
                    else
                    {
                        // We hit an enemy, but there's no script attached to it.
                        Debug.Log("Enemy Script not present");
                    }
                }
            }
        }
        else
        {
            // Attack timer needs count-down
            attackCountdownTimer -= Time.deltaTime;
        }
    }

    // Used to draw a circle when we are selecting the player in the scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
