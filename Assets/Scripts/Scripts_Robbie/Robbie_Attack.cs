using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robbie_Attack : MonoBehaviour
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

     public BoxCollider2D bc2d;
     public float boxCastDistance;
     bool canMove;

     void Update()
     {

         canMove = gameObject.GetComponent<Character_Robbie>().canMove;

         // See if we can attack, via timer.
         if (attackCountdownTimer <= 0)
         {
             // We can attack. See if user hit space bar.
             if (Input.GetKey(KeyCode.J))
             {
                 // Reset the countdown timer
                 attackCountdownTimer = attackTimeLimit;
                 // What enemies did we hit?
                RaycastHit2D hit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, boxCastDistance, enemyLayer);

                if (!hit)
                {
                    hit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.up, boxCastDistance, enemyLayer);
                }
                if (!hit)
                {
                    hit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.right, boxCastDistance, enemyLayer);
                }
                if (!hit)
                {
                    hit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.left, boxCastDistance, enemyLayer);
                }
                if (hit && canMove)
                {
                 // Loop through each enemy we hit
                     // Get the enemy script attached to this object
                     PlantedPumpkin enemyScript = hit.transform.gameObject.GetComponent<PlantedPumpkin>();
                     // If there is an enemy script
                     if (enemyScript)
                     {
                         // Damage
                         if(enemyScript.health > 1) StartCoroutine(enemyScript.ShakePumpkin());
                         enemyScript.health -= damage;
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
