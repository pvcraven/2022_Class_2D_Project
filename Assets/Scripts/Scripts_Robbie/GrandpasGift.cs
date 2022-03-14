using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandpasGift : MonoBehaviour
{
    public IEnumerator GiftPlayer(Character_Robbie character)
    {
        yield return new WaitForSeconds(33f);
        character.score += 1000000;
        character.scoreText.text = "Pumpkin Points: " + character.score;
        StopCoroutine(GiftPlayer(character));
    }
}
