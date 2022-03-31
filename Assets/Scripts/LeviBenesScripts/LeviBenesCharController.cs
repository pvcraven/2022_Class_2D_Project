using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeviBenesCharController : MonoBehaviour
{
    public int score = 0;

    Rigidbody2D body;

    // Movement Variables
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float runSpeed = 5.0f;

    // Bullet Variables
    public GameObject bulletPrefab;
    public float bulletSpeed;

    // Sound Variables
    public AudioSource sound;
    public AudioSource increaseScoreSound;
    public AudioSource decreaseScoreSound;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        // Get the rigid body component for the player character.
        // (required to have one)
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    // Get vector angle
    public float GetYRotFromVec(Vector2 v1)
    {
        float _r = Mathf.Atan2(v1.y, v1.x);
        float _d = (_r / Mathf.PI) * 180;
        return _d;
    }
    void Update()
    {
        // Get our axis values
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Has the mouse been pressed?
        if (Input.GetMouseButtonDown(0))
        {
            var bullet = Instantiate(bulletPrefab, body.position, Quaternion.identity);
            var bulletbody = bullet.GetComponent<Rigidbody2D>();
            var mousePos = Input.mousePosition;

            Vector3 target3 = Camera.main.ScreenToWorldPoint(mousePos);
            target3.z = 0;
            Vector2 direction = (target3 - transform.position).normalized;

            float angle = GetYRotFromVec(direction);

            bulletbody.rotation = angle;
            bulletbody.velocity = direction * bulletSpeed;
        }
    }

    void FixedUpdate()
    {

        // If player is running diagonally, we don't want them to move extra-fast.
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        if (horizontal > 0.1)
        {
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
        } else if (horizontal < -0.1)
        {
            //spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-1,1,1);
        }

        // Set player velocity
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
        animator.SetFloat("VerticalSpeed", Mathf.Abs(vertical));
    }

    void OnTriggerEnter2D(Collider2D colliderEvent)
    {
        // Did we run into an object that will affect our score?
        ScoreScript scoreObject = colliderEvent.gameObject.GetComponent(typeof(ScoreScript))
                                  as ScoreScript;

        if (scoreObject != null)
        {
            // Yes, change the score
            score += scoreObject.points;
            // Determine if object increases or decreases score
            if (scoreObject.points >= 0)
            {
                increaseScoreSound.Play();
            }
            if (scoreObject.points < 0)
            {
                decreaseScoreSound.Play();
            }
            // Destroy the object
            Destroy(colliderEvent.gameObject);
        }

        // Did we run into an object that will cause a scene change?
        SceneChangeScript sceneChangeObject = colliderEvent.gameObject.GetComponent(typeof(SceneChangeScript))
                                              as SceneChangeScript;
        if (sceneChangeObject != null)
        {
            // Yes, get our current scene index
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Load up the scene accourding to the sceneChange value
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + sceneChangeObject.sceneChange);
        }
    }
    void OnGUI()
    {
        // Dispaly our score
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 32; //modify the font height
        GUI.Label(new Rect(10, 10, 250, 50), "Score: " + score, guiStyle);
    }
}
