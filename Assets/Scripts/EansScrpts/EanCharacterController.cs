using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EanCharacterController : MonoBehaviour
{
    public int score = 0;
    public int health = 3;
    public int maxHealth = 3;
    public int lives = 3;
    public AudioSource winSound = null;
    public AudioSource gameOverSound = null;


    Rigidbody2D body;
    SpriteRenderer spriteRender;

    float horizontal;
    float vertical;

    public float runSpeed;
    public float jumpHeight;
    public AudioSource coinSound;

    private bool canJump = true;
    private Animator animator;

    void Start()
    {
        // Get the rigid body component for the player character.
        // (required to have one)
        body = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get our axis values
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown("w") && canJump)
        {
            //canJump = false;
            Debug.Log("Jump!");
            body.velocity = new Vector2(horizontal * runSpeed, jumpHeight);
        } else
        {
            body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);
        }
    }

    void FixedUpdate()
    {
        animator.SetBool("Walking", body.velocity.x != 0);
        if (body.velocity.x == 0)
        { 
            spriteRender.flipX = false;
        } else if (body.velocity.x > 0)
        {
            spriteRender.flipX = false;
        } else
        {
            spriteRender.flipX = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EanGround")
        {
            canJump = true;
        } else if (collision.gameObject.tag == "EanDeadly")
        {
            gameOverSound.Play();
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Reload the scene 
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EanGround")
        {
            canJump = false;
        }
    }

    void OnTriggerEnter2D(Collider2D colliderEvent)
    {
        // Did we run into an object that will affect our score?
        EanCoin scoreObject = colliderEvent.gameObject.GetComponent(typeof(EanCoin))
                                  as EanCoin;

       
        if (colliderEvent.gameObject.tag == "EanGround")
        {
            canJump = true;
        }

        if (scoreObject != null)
        {
            // Yes, change the score
            score += scoreObject.points;
            coinSound.Play();
            // Destroy the object
            Destroy(colliderEvent.gameObject);
            
        }

        // Did we run into an object that will cause a scene change?
        SceneChangeScript sceneChangeObject = colliderEvent.gameObject.GetComponent(typeof(SceneChangeScript))
                                              as SceneChangeScript;
        if (sceneChangeObject != null)
        {
            winSound.Play();
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
        guiStyle.fontSize = 24; //modify the font height
        GUI.Label(new Rect(10, 10, 100, 50), "Score: " + score, guiStyle);
    }
}
