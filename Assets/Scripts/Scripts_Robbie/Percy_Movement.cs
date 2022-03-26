using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percy_Movement : MonoBehaviour
{
    bool movingLeft;
    public float speed;

    public float distanceLimit;
    float minX;
    float maxX;

    public Animator percysAnimator;
    public bool interacting;
    public SpriteRenderer percyRenderer;

    void Start()
    {
        percysAnimator.SetBool("Walking", true);

        movingLeft = true;

        speed = speed/1000;

        minX = gameObject.transform.position.x - distanceLimit;
        maxX = gameObject.transform.position.x + distanceLimit;
    }

    void Update()
    {
        if(!interacting)
        {
            if(transform.position.x < minX)
            { 
                movingLeft = true;
                percyRenderer.flipX = false;
            }
            else if(transform.position.x > maxX){
                movingLeft = false;
                percyRenderer.flipX = true;
            }

            if(movingLeft) transform.Translate(speed, 0, 0);
            else transform.Translate(-speed, 0, 0);
        }
    }

    public IEnumerator BeginWalkingAgain()
    {
        percysAnimator.SetBool("Walking", false);
        yield return new WaitForSeconds(30f);
        interacting = false;
        percysAnimator.SetBool("Walking", true);
        StopCoroutine(BeginWalkingAgain());
    }
}
