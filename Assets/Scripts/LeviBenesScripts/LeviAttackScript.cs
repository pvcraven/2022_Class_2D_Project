using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviAttackScript : MonoBehaviour
{
    public float attackSpeed = 0.5f;
    private float attackCD = 0;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayer;
    public int damage = 3;

    // Sword rotating animation variables
    //public Vector2 weaponOffset;
    //public float weaponRotation = 135;
    //public float swingSpeed = 10;

    //int swing = 1;
    //GameObject anchor;
    //Vector3 swingTarget;
    //float swingAngle;
    //bool isSwinging;

    void Start()
    {
        //anchor = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Anchor rotation
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //Vector3 rotation = anchor.transform.eulerAngles;
        //swingAngle = Mathf.Lerp(swingAngle, swing * 90, Time.deltaTime * swingSpeed);
        //rotation.z = angle + swingAngle;
        //anchor.transform.eulerAngles = rotation;

        // Weapon rotation
        //float t = swing == 1 ? 45 : -255;
        //swingTarget.z = Mathf.Lerp(swingTarget.z, t, Time.deltaTime * swingSpeed);
        //if (Mathf.Abs(t - swingTarget.z) < 5) isSwinging = false;
        //transform.localRotation = Quaternion.Euler(swingTarget);

        if (attackCD <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //if (isSwinging) return;
                //swing *= -1;
                //isSwinging = true;

                Debug.Log("Attack");
                attackCD = attackSpeed;

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
                for (int i=0; i < enemiesToDamage.Length; i++)
                {
                    LeviNPCMove enemyScript = enemiesToDamage[i].GetComponent<LeviNPCMove>();
                    if (enemyScript)
                    {
                        enemyScript.health -= damage;
                        Debug.Log(enemyScript.health);
                        // Destroy object when 0 health
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
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
