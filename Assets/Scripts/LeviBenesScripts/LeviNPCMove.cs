using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeviNPCMove : MonoBehaviour
{
    Rigidbody2D npcrb;

    public int currentHealth;
    public int maxHealth = 10;
    public LeviHealthBar healthBar;

    public float moveSpeed;
    public bool isWalking;

    public float walkTime;
    private float walkCounter;

    public float waitTime;
    private float waitCounter;

    private int WalkDirection;

    private SpriteRenderer npcSpriteRenderer;
    private Animator npcAnimator;

    // Start is called before the first frame update
    void Start()
    {
        npcrb = GetComponent<Rigidbody2D>();
        npcSpriteRenderer = GetComponent<SpriteRenderer>();
        npcAnimator = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            npcAnimator.Play("LeviPineappleRunAnim");
            walkCounter -= Time.deltaTime;

            if(walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }

            switch(WalkDirection)
            {
                // Decide direction, 0 Up, 1 Right, 2 Down, 3 Left
                case 0:
                    npcrb.velocity = new Vector2(0, moveSpeed);
                    break;
                case 1:
                    npcrb.velocity = new Vector2(moveSpeed, 0);
                    npcSpriteRenderer.flipX = false;
                    break;
                case 2:
                    npcrb.velocity = new Vector2(0, -moveSpeed);
                    break;
                case 3:
                    npcrb.velocity = new Vector2(-moveSpeed, 0);
                    npcSpriteRenderer.flipX = true;
                    break;
            }
        } else
        {
            npcAnimator.Play("LeviPineappleIdleAnim");
            waitCounter -= Time.deltaTime;
            npcrb.velocity = Vector2.zero;
            if(waitCounter < 0)
            {
                ChooseDirection();
            }
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Melee Destroy");
        }
    }

    public void ChooseDirection()
    {
        WalkDirection = Random.Range (0,4);
        isWalking = true;

        walkCounter = walkTime;
        waitTime = Random.Range(0, 5);
    }
}
