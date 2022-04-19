using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviAttackScript : MonoBehaviour
{
    public float attackSpeed = 0.5f;
    private float attackCD = 0;
    //public float kbForce;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayer;
    public int damage = 3;

    private Animator swordAnimator;
    private GameObject weaponChild;

    void Start()
    {
        weaponChild = GameObject.Find("LeviFireSword_0");
        swordAnimator = weaponChild.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCD <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                swordAnimator.SetTrigger("swordSwing");

                Debug.Log("Attack");
                attackCD = attackSpeed;

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
                for (int i=0; i < enemiesToDamage.Length; i++)
                {
                    LeviNPCMove enemyScript = enemiesToDamage[i].GetComponent<LeviNPCMove>();
                    if (enemyScript)
                    {
                        enemyScript.currentHealth -= damage;
                        enemyScript.healthBar.SetHealth(enemyScript.currentHealth);
                        Debug.Log(enemyScript.currentHealth);
                        // Object destruction handled in NPC script

                        // Knockback
                        //Rigidbody2D enemy = enemyScript.GetComponent<Rigidbody2D>();
                        //Debug.Log("Enemy: " + enemy.position);
                        //enemy.isKinematic = false;
                        //Vector2 posDiff = enemy.transform.position - transform.position;
                        //posDiff = posDiff.normalized * kbForce;
                        //enemy.AddForce(posDiff, ForceMode2D.Impulse);
                        //enemy.isKinematic = true;
                    } else
                    {
                        Debug.Log("No enemy script");
                    }
                }
            }
        }
        else
        {
            attackCD -= Time.deltaTime;
            swordAnimator.SetTrigger("swordSwing");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
