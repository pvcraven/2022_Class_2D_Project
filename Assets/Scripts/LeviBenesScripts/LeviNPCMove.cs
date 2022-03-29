using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeviNPCMove : MonoBehaviour
{
    Rigidbody2D npcrb;

    float npcHorizontal;
    float npcVertical;
    float speed;
    Vector3 lastPos = Vector3.zero;

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

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Get our axis values
        speed = (transform.position - lastPos).magnitude;
        npcAnimator.SetFloat("npcHorizontalSpeed", speed);
        npcAnimator.SetFloat("npcVerticalSpeed", speed);
        Debug.Log("speed: " + speed);
        Debug.Log("npcHS: " + npcAnimator.GetParameter("npcHorizontalSpeed"));
        Debug.Log("npcVS: " + npcAnimator.GetParameter("npcVerticalSpeed"));

        if (isWalking)
        {
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
                    break;
                case 2:
                    npcrb.velocity = new Vector2(0, -moveSpeed);
                    break;
                case 3:
                    npcrb.velocity = new Vector2(-moveSpeed, 0);
                    break;
            }
        } else
        {
            waitCounter -= Time.deltaTime;
            npcrb.velocity = Vector2.zero;
            if(waitCounter < 0)
            {
                ChooseDirection();
            }
        }

        if (npcHorizontal > 0.1)
        {
            npcSpriteRenderer.flipX = false;
        }
        else if (npcHorizontal < -0.1)
        {
            npcSpriteRenderer.flipX = true;
        }

        // Set player velocity
        npcAnimator.SetFloat("npcHorizontalSpeed", Mathf.Abs(npcHorizontal));
        npcAnimator.SetFloat("npcVerticalSpeed", Mathf.Abs(npcVertical));
    }

    public void ChooseDirection()
    {
        WalkDirection = Random.Range (0,4);
        isWalking = true;

        walkCounter = walkTime;
    }
}
