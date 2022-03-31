using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZachWemhoffCharacterScript : MonoBehaviour
{
    public int score = 0;

    Rigidbody2D body;


    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 5.0f;

    public AudioSource coinSound;
    public AudioSource negativeSound;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public GameObject bulletPrefab;

    float bulletSpeed = 5.0f;

    // Get the angle of a vector
    public float GetYRotFromVec(Vector2 v1)
    {
        float _r = Mathf.Atan2(v1.y, v1.x);
        float _d = (_r / Mathf.PI) * 180;

        return _d;
    }

    void Start()
    {
        // Get the rigid body component for the player character.
        // (required to have one)
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get our axis values
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Mouse pressed?
        if (Input.GetMouseButtonDown(0))
        {
            // -- Fire a bullet

            // Create the bullet
            var bullet = Instantiate(bulletPrefab, body.position, Quaternion.identity);
            // Get a reference to the bullet's rigid body
            var bulletbody = bullet.GetComponent<Rigidbody2D>();
            // Where is the mouse on the screen?
            var mousePosition = Input.mousePosition;
            // Where is the mouse in the world?
            Vector3 target3 = Camera.main.ScreenToWorldPoint(mousePosition);
            // Set the z value of this vector 3
            target3.z = 0;
            // What is the normalized vector from the player to the mouse?
            Vector2 direction = (target3 - transform.position).normalized;
            // What is the angle in degrees?
            float angle = GetYRotFromVec(direction);
            // Rotate the bullet
            bulletbody.rotation = angle;
            // Give the bullet speed
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
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;

        // Set player velocity
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
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
            if (scoreObject.points > 0)
            {
                coinSound.Play();
            }
            if (scoreObject.points < 0)
            {
                negativeSound.Play();
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
