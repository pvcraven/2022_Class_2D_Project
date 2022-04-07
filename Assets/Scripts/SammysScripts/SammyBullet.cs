using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SammyBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.tag == "Destroyable")
        {

            Debug.Log("Destroyable");
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
