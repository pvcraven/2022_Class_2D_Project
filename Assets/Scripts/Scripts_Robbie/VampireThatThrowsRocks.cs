using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireThatThrowsRocks : MonoBehaviour
{

    public Rigidbody2D body;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Character_Robbie character;
    private bool throwingRock;
    public Transform vampire;

    public float GetYRotFromVec(Vector2 v1)
    {
        float _r = Mathf.Atan2(v1.y, v1.x);
        float _d = (_r / Mathf.PI) * 180;

        return _d;
    }

    void Start()
    {
        throwingRock = false;
    }

    void Update()
    {
        if(character.canMove && !throwingRock) StartCoroutine(ThrowRock());
        if(!character.canMove)
        {
            foreach(Transform rock in gameObject.transform)
            {
                RockThrow rockThrow = rock.GetComponent<RockThrow>();
                if(rockThrow != null) rockThrow.destroy();
            }
        }
    }

    IEnumerator ThrowRock()
    {
        throwingRock = true;
        // Create the bullet
        var bullet = Instantiate(bulletPrefab, body.position, Quaternion.identity);
        bullet.transform.parent = vampire;
        // Get a reference to the bullet's rigid body
        var bulletbody = bullet.GetComponent<Rigidbody2D>();
        // Where is the mouse on the screen?
        var mousePosition = Input.mousePosition;
        // Where is the mouse in the world?
        Vector3 target3 = Camera.main.ScreenToWorldPoint(mousePosition);
        // Set the z value of this vector 3
        target3.z = 0;
        // What is the normalized vector from the player to the mouse?
        Vector2 direction = (target3 - transform.position).normalized;
        // What is the angle in degrees?
        float angle = GetYRotFromVec(direction);
        // Rotate the bullet
        bulletbody.rotation = angle;
        // Give the bullet speed
        bulletbody.velocity = direction * bulletSpeed;
        yield return new WaitForSeconds(2f);
        throwingRock = false;
        StopCoroutine(ThrowRock());
    }
}
