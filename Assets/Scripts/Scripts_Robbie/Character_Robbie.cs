using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character_Robbie : MonoBehaviour
{
    // Code adapted from MyCharacterController (Dr.Craven)

    public int score = 0;

    Rigidbody2D body;

    bool canMove;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 5.0f;
    public GameObject scoreGO;
    public Text scoreText;
    public Text dialogueText;
    public string dialoguePath;

    void Start()
    {
        // Get the rigid body component for the player character.
        // (required to have one)
        body = GetComponent<Rigidbody2D>();

        scoreText.fontSize = 20;

        canMove = false;
        StartCoroutine(OpeningDialogue());
    }

    void Update()
    {
        if(canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal"); 
            vertical = Input.GetAxisRaw("Vertical"); 
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

        // Set player velocity
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
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
            // Destroy the object
            Destroy(colliderEvent.gameObject);
            scoreText.text = "Pumpkin Points: " + score;
        }

        // Did we run into an object that will cause a scene change?
        SceneChangeScript sceneChangeObject = colliderEvent.gameObject.GetComponent(typeof(SceneChangeScript))
                                              as SceneChangeScript;
        if (sceneChangeObject != null) {
            // Yes, get our current scene index
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Load up the scene accourding to the sceneChange value
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + sceneChangeObject.sceneChange);
        }
    }

    IEnumerator OpeningDialogue()
    {
        // creates a streamreader object and reads the opening dialogue
        string line;
        StreamReader readDialogue = new StreamReader(dialoguePath);
        scoreGO.SetActive(false);
        dialogueText.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);

        while((line = readDialogue.ReadLine()) != " ")
        {
            dialogueText.text = line;
            yield return new WaitForSeconds(.5f);
            while((line = readDialogue.ReadLine()) != "")
            {
                dialogueText.text = line;
                yield return new WaitForSeconds(.5f);
            }
        }

        yield return new WaitForSeconds(1f);
        scoreGO.SetActive(true);
        dialogueText.gameObject.SetActive(false);
        canMove = true;
        Debug.Log(scoreGO.activeSelf);
        StopCoroutine(OpeningDialogue());
    }
}
