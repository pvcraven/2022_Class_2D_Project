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

     // How much damage to deal
     public int damage = 3;

    //character information needed
     public Character_Robbie character;
     BoxCollider2D bc2d;

     //information needed for boxcast
     public float boxCastDistance;
     public LayerMask interactable;

     void Start()
     {
         bc2d = GetComponent<BoxCollider2D>();
     }

     void Update()
     {
         // See if we can attack, via timer.
         if (attackCountdownTimer <= 0)
         {
             // We can attack. See if user hit space bar.
             if(Input.GetKeyDown("j"))
            {
                RaycastHit2D hit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, boxCastDistance, interactable);
                if (!hit)
                {
                    hit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.up, boxCastDistance, interactable);
                }
                if (!hit)
                {
                    hit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.right, boxCastDistance, interactable);
                }
                if (!hit)
                {
                    hit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.left, boxCastDistance, interactable);
                }
                if (hit && character.canMove)
                {
                    character.canMove = false;

                }
            }
         }
         else
         {
             // Attack timer needs count-down
             attackCountdownTimer -= Time.deltaTime;
         }
     }
}
