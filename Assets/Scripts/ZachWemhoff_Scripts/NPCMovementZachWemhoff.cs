using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCMovementZachWemhoff : MonoBehaviour
{
    public float min;
    public float max;
    public float xLeft = 0.0f;
    public float xRight = 0.0f;

    float horizontal;
    float vertical;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
       min = transform.position.x - xLeft;
       max = transform.position.x + xRight;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        if (horizontal > 0.1)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;

        animator.SetFloat("Horizontal", horizontal);
    }
}
