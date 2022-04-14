using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZachAmbrose_Attack_Script : MonoBehaviour
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
    // Update is called once per frame
    void Update()
    {
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
                    ZachAmbrose_Enemy_Script enemyScript = enemiesToDamage[i].GetComponent<ZachAmbrose_Enemy_Script>();
                    // If there is an enemy script
                    if (enemyScript)
                    {
                        // Deal Damage
                        enemiesToDamage[i].GetComponent<ZachAmbrose_Enemy_Script>().health -= damage;
                        Debug.Log(enemiesToDamage[i].GetComponent<ZachAmbrose_Enemy_Script>().health);

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
    // Used to draw circle hwen selecting player in scene view
    void OnDrawGizmosSelected()
    {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
