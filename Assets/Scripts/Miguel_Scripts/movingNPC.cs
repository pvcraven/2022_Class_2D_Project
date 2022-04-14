using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingNPC : MonoBehaviour
{
    private Vector3 curPosition;
    
    public float magnitude = 4f;
    public float waitTime = 3f;
    public int health = 12;

    private float idleTimer = 0f;
    private float movingTimer = 0f;
    

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        curPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (idleTimer > waitTime) {
            if (movingTimer < (waitTime/2)){
                transform.position = curPosition + transform.right * movingTimer * magnitude;
            }
            else {
                transform.position = curPosition + transform.right * (waitTime - movingTimer) * magnitude;
            }

            movingTimer += Time.deltaTime;
        }
        else {
            movingTimer = 0f;
        }

        if (movingTimer > waitTime) {
            idleTimer = 0f;
        }

        idleTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(movingTimer));
    }
}
