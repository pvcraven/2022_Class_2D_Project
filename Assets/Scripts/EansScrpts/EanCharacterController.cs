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
    public float bulletSpeed;
    public AudioSource winSound = null;
    public AudioSource gameOverSound = null;
    public GameObject bulletPrefab;
    public LayerMask enemyLayer;

    Rigidbody2D body;
    SpriteRenderer spriteRender;

    float horizontal;
    float vertical;

    public float runSpeed;
    public float jumpHeight;
    public AudioSource coinSound;

    private Animator animator;
    private float delta = 0;

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
        delta += 1;
        // Get our axis values
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown("w") && (body.velocity.y < .001 && body.velocity.y > -.001))
        {
            body.velocity = new Vector2(horizontal * runSpeed, jumpHeight);
        } else
        {
            body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);
        }

        if (Input.GetKey(KeyCode.Space) && delta >= 60)
        {
            delta = 0;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, 1, enemyLayer);
            // Loop through each enemy we hit
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                Spikey enemyScript = enemiesToDamage[i].GetComponent<Spikey>();
                if (enemyScript != null)
                {
                    Debug.Log("Hit!");
                    Destroy(enemiesToDamage[i].gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            var mousePosition = Input.mousePosition;
            // Where is the mouse in the world?
            Vector3 target3 = Camera.main.ScreenToWorldPoint(mousePosition);
            //Debug.Log("Mouse down!");

            Vector3 delta3 = target3 - transform.position;
            delta3.z = 0;
            //Debug.Log(delta3);
            var bullet = Instantiate(bulletPrefab, body.position, Quaternion.identity);
            // Get a reference to the bullet's rigid body
            var bulletbody = bullet.GetComponent<Rigidbody2D>();
            bulletbody.velocity = delta3.normalized * bulletSpeed;
        }
    }

    void FixedUpdate()
    {
       
        if (body.velocity.x >= -.01 && body.velocity.x <= .01)
        {
            animator.SetBool("Walking", false);
            spriteRender.flipX = false;
        } else if (body.velocity.x > 0)
        {
            animator.SetBool("Walking", true);
            spriteRender.flipX = false;
        } else
        {
            animator.SetBool("Walking", true);
            spriteRender.flipX = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EanDeadly")
        {
            gameOverSound.Play();
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Reload the scene 
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
        }
    }

    void OnTriggerEnter2D(Collider2D colliderEvent)
    {
        // Did we run into an object that will affect our score?
        EanCoin scoreObject = colliderEvent.gameObject.GetComponent(typeof(EanCoin))
                                  as EanCoin;


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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }


}
