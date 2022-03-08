using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dan_enemy_script : MonoBehaviour
{
    public GameObject daniel_FireSlimeEnemy;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, 1) * 6 - 3;
        gameObject.transform.position[1] = y;
    }
}
