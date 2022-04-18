using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miguelAttack : MonoBehaviour
{
    // How frequently can we attack?
    private float attackTimeLimit = 0.8f;

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

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // See if we can attack, via timer.
        if (attackCountdownTimer <= 0)
        {
            // We can attack. See if user hit space bar.
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetInteger("Attacking", 1);
                Debug.Log("Attack");
                // Reset the countdown timer
                attackCountdownTimer = attackTimeLimit;
                // What enemies did we hit?
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
                // Loop through each enemy we hit
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    // Get the enemy script attached to this object
                    movingNPC enemyScript = enemiesToDamage[i].GetComponent<movingNPC>();
                    // If there is an enemy script
                    if (enemyScript)
                    {
                        // Damage
                        enemiesToDamage[i].GetComponent<movingNPC>().health -= damage;
                        // Print health levels
                        Debug.Log(enemiesToDamage[i].GetComponent<movingNPC>().health);

                        // --- ToDo: destroy enemy here when health <= 0
                        if (enemiesToDamage[i].GetComponent<movingNPC>().health <= 0)
                        {
                            Destroy(enemiesToDamage[i].gameObject);
                        }
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

        if (attackCountdownTimer <= 0.1)
        {
            animator.SetInteger("Attacking", 0);
        }

    }
    // Used to draw a circle when we are selecting the player in the scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}