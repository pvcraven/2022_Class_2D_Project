using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peter_Attack_Script : MonoBehaviour
{
    // How frequently can we attack?
    public float attackTimeLimit = 0.5f;

    // Countdown timer for attacks
    private float attackCountdownTimer = 0;

    // Blood Burst Prefab
    public GameObject burstPrefab;

    // An empty parented that says where to attack
    public Transform attackPos;
    // Radius of attack circle
    public float attackRange;
    // What layer will the enemies be on?
    public LayerMask enemyLayer;
    // How much damage to deal
    public int damage = 3;

    private Animator swordAnimator;
    private GameObject weaponChild;

    void Start()
    {
        weaponChild = GameObject.Find("PeterSword");
        swordAnimator = weaponChild.GetComponent<Animator>();
    }


    void Update()
    {
        // See if we can attack, via timer.
        if (attackCountdownTimer <= 0)
        {
            // We can attack. See if user hit space bar.
            if (Input.GetKey(KeyCode.X))
            {
                Debug.Log("Attack");
                swordAnimator.SetTrigger("swordSwing");
                // Reset the countdown timer
                attackCountdownTimer = attackTimeLimit;
                // What enemies did we hit?
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
                // Loop through each enemy we hit
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    // Get the enemy script attached to this object
                    Peter_Enemy_Script enemyScript = enemiesToDamage[i].GetComponent<Peter_Enemy_Script>();
                    // If there is an enemy script
                    if (enemyScript)
                    {
                        // Damage
                        enemyScript.health -= damage;
                        // Print health levels
                        Debug.Log(enemyScript.health);
                        var burst = Instantiate(burstPrefab, attackPos.position, Quaternion.identity);
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
