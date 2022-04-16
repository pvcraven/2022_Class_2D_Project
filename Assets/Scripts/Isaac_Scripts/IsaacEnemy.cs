using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaacEnemy : MonoBehaviour
{
    public int health;
    public float speed;

    private Transform target;

    //private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // anim = GetComponent<Animator>();
        // anim.SetBool("isRunning", true);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        //transform.Translate(Vector2.left * speed * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage Taken");
    }
}
