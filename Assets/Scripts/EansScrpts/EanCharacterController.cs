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
    public Sprite stillSprite = null;
    public Sprite rightSprite = null;
    public Sprite leftSprite = null;

    Rigidbody2D body;
    SpriteRenderer spriteRender;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 5.0f;
    public float jumpHeight = 5.0f;

    private bool canJump = true;
    

    void Start()
    {
        // Get the rigid body component for the player character.
        // (required to have one)
        body = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get our axis values
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown("w") && canJump)
        {
            canJump = false;
            Debug.Log("Jump!");
            body.velocity = new Vector2(horizontal * runSpeed, jumpHeight);
        } else
        {
            body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);
        }
    }

    void FixedUpdate()
    {
        if (body.velocity.x == 0)
        {
            spriteRender.sprite = stillSprite;
        } else if (body.velocity.x > 0)
        {
            spriteRender.sprite = rightSprite;
        } else
        {
            spriteRender.sprite = leftSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D colliderEvent)
    {
        // Did we run into an object that will affect our score?
        ScoreScript scoreObject = colliderEvent.gameObject.GetComponent(typeof(ScoreScript))
                                  as ScoreScript;

        if (colliderEvent.gameObject.tag == "Respawn")
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Reload the scene 
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
        } else if (colliderEvent.gameObject.tag == "EanGround")
        {
            canJump = true;
        }

        if (scoreObject != null)
        {
            // Yes, change the score
            score += scoreObject.points;
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
        guiStyle.fontSize = 24; //modify the font height
        GUI.Label(new Rect(10, 10, 100, 50), "Score: " + score, guiStyle);
    }
}
