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

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            character.score += scoreHandOff;
            Destroy(gameObject);
        }
    }

    public IEnumerator ShakePumpkin()
    {
        yield return new WaitForSeconds(1f);
        StopCoroutine(ShakePumpkin());
    }
}
