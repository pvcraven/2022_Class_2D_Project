using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WemhoffEnemyScript : MonoBehaviour
{
    public int health;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        //Instantiate(transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("damage TAKEN!");
    }
}
