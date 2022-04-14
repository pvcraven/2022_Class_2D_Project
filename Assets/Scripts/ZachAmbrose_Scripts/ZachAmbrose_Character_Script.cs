using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZachAmbrose_Character_Script : MonoBehaviour
{
    public int score = 0;

    Rigidbody2D body;
    public GameObject projectilePrefab;
    public GameObject playerStaff;

    private SpriteRenderer spriteRenderer;
    private Animator animator;


    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public Vector3 spawnPosition;
    public float runSpeed = 5.0f;

    public float projectileSpeed;

    public AudioClip increaseScoreSound;
    public AudioClip enemyDeathSound;
    public AudioClip deathSound;


    void Start()
    {
        // Get the rigid body component for the player character.
        // (required to have one)
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerStaff.SetActive(false);
    }

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

        if (score >= 20)
        {
            
            playerStaff.SetActive(true);
            
            if (Input.GetMouseButtonDown(0))
            {

                var projectile = Instantiate(projectilePrefab, body.position, Quaternion.identity);

                var projectileBody = projectile.GetComponent<Rigidbody2D>();

                var mousePosition = Input.mousePosition;

                Vector3 target3 = Camera.main.ScreenToWorldPoint(mousePosition);

                target3.z = 0;

                Vector2 direction = (target3 - transform.position).normalized;

                float angle = GetYRotFromVec(direction);

                projectileBody.rotation = angle;

                projectileBody.velocity = direction * projectileSpeed;
            }
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
            spriteRenderer.flipX = false;
        }

        else if (horizontal < -0.1)
        {
            spriteRenderer.flipX = true;
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

            if (scoreObject.points > 0)
            {
                //Play score increase sound
                AudioSource.PlayClipAtPoint(increaseScoreSound, transform.position);

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
        guiStyle.fontSize = 20; //modify the font height
        GUI.Label(new Rect(50, 40, 100, 50), "Score: " + score, guiStyle);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        bool Enemy = col.gameObject.tag.Equals("Enemy");
        if (Enemy)
        {
            gameObject.transform.position = spawnPosition;
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
    }
}
