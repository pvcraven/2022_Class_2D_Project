using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Move_Isaac : MonoBehaviour
{
    public float min;
    public float max;
    public float yUp = 0.0f;
    public float yDown = 0.0f;

    float vertical;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.y - yDown;
        max = transform.position.y + yUp;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.z);

        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        animator.SetFloat("Vertical", vertical);
    }
}
