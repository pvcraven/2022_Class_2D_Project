using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantedPumpkin : MonoBehaviour
{

    public Character_Robbie character;
    public float health;

    public GameObject pumpkinPrefab;
    public Rigidbody2D body;

    public int scoreHandOff;

    public Animator pumpkinAnimator;

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            character.score += scoreHandOff;
            StartCoroutine(character.TurnOnScoreTemporarily());
            character.scoreText.text = "Pumpkin Points: " + character.score;
            Destroy(gameObject);
        }
    }

    public IEnumerator ShakePumpkin()
    {
        pumpkinAnimator.SetBool("Shaking", true);
        yield return new WaitForSeconds(.4f);
        pumpkinAnimator.SetBool("Shaking", false);
        StopCoroutine(ShakePumpkin());
    }
}
