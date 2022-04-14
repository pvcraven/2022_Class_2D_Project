using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Peter_Controller_Script : MonoBehaviour
{
    Rigidbody2D body;

    int score = 0;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public Vector3 respawnPos;
    public float runSpeed = 5.0f;

    // Audio variables
    public AudioSource death_audio;
    public AudioSource positive_pickup_audio;
    public AudioSource negative_pickup_audio;

    // Text Instruction Variables
    public Text dialogueText;
    public string sword_instruction_path;
    public string bow_instruction_path;

    // For my sword
    public GameObject sword_child;
    public SpriteRenderer sword_sprite;
    private Vector3 right_sword_pos = new Vector3(.55f, 0f, 0f);
    private Vector3 left_sword_pos = new Vector3(-.55f, 0f, 0f);

    // For my bow and arrow animation
    public GameObject bow_child;
    public SpriteRenderer bow_arrow;
    public Animator bowAnimator;
    private Vector3 right_bow_pos = new Vector3(.51f, 0f, 0f);
    private Vector3 left_bow_pos = new Vector3(-.51f, 0f, 0f);
    private bool ranger_upgrade = false;
    
    // For my hit box
    public GameObject hit_box_child;
    private Vector3 right_hit_box_pos = new Vector3(.9f, -.16f, 0f);
    private Vector3 left_hit_box_pos = new Vector3(-.9f, -.16f, 0f);

    // Character animation variables
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        // Making the bow start "inactive"
        bow_child.SetActive(false);

        // Playing the text intro
        StartCoroutine(ReadInstructions(new StreamReader(sword_instruction_path)));

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

        // Dealing with firing my bow
        if (Input.GetKeyDown("space"))
        {
            // Set bow child to true if ranger upgrade and sword to false
            if(ranger_upgrade)
            {
                bow_child.SetActive(true);
                sword_child.SetActive(false);
            }
            bowAnimator.SetBool("FireKey", true);
        }
        if (Input.GetKeyDown("x"))
        {
            bow_child.SetActive(false);
            sword_child.SetActive(true);
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

        // Fliping objects when moving side to side
        if (horizontal > 0.1)
        {
            spriteRenderer.flipX = true;

            // Moving and fliping bow
            bow_arrow.flipX = false;
            bow_child.transform.position = gameObject.transform.position + right_bow_pos;

            //Moving meelee hit box
            hit_box_child.transform.position = gameObject.transform.position + right_hit_box_pos;
            sword_child.transform.position = gameObject.transform.position + right_sword_pos;
            sword_child.transform.rotation = Quaternion.Euler(0f, 0f, -30f);
            sword_sprite.flipX = false;
        }
        if (horizontal < -0.1)
        {
            spriteRenderer.flipX = false;

            // Moving and flipping bow
            bow_arrow.flipX = true;
            bow_child.transform.position = gameObject.transform.position + left_bow_pos;

            //Moving meelee hit box
            hit_box_child.transform.position = gameObject.transform.position + left_hit_box_pos;
            sword_child.transform.position = gameObject.transform.position + left_sword_pos;
            sword_child.transform.rotation = Quaternion.Euler(0f, 0f, 30f);
            sword_sprite.flipX = true;
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
            // Playing 2 dif sounds if positive pickup or negative pickup
            if (scoreObject.points >= 0)
            {
                positive_pickup_audio.Play();
            }
            if (scoreObject.points < 0)
            {
                negative_pickup_audio.Play();
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
        guiStyle.fontSize = 24; //modify the font height
        GUI.Label(new Rect(10, 20, 100, 50), "Score: " + score, guiStyle);
    }

    // Handeling enemy collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            // Respawning player at start (does not reset pickups)
            gameObject.transform.position = respawnPos;
            death_audio.Play();
        }

        // Used to enable the bow which you don't start with
        if (col.gameObject.tag.Equals("RangerUpgrade"))
        {
            bow_child.SetActive(true);
            sword_child.SetActive(false);
            Destroy(col.gameObject);
            ranger_upgrade = true;
            // Playing the bow instructions
            StartCoroutine(ReadInstructions(new StreamReader(bow_instruction_path)));
        }
    }

    IEnumerator ReadInstructions(StreamReader dialogueReader)
    {
        // This code is adapted from Robbie's code

        string line;
        dialogueText.gameObject.SetActive(true);

        while ((line = dialogueReader.ReadLine()) != " ")
        {
            dialogueText.text = line;
            yield return new WaitForSeconds(.5f);
            while ((line = dialogueReader.ReadLine()) != "")
            {
                dialogueText.text = line;
                yield return new WaitForSeconds(.5f);
            }
        }

        yield return new WaitForSeconds(1f);

        // Close the reader and the coroutine
        dialogueReader.Close();
        StopCoroutine(ReadInstructions(dialogueReader));
    }

}
