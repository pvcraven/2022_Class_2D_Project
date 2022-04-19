using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class eanBulletScript : MonoBehaviour
{
    public float speed;
    private int frame;
    private int lifetime = 12;
    private Tilemap map;
    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
        map = GameObject.Find("DestroyNew").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        frame = frame + 1;
        if (frame >= lifetime * 60)
        {
            Debug.Log("Delete!");
            Destroy(gameObject);
        }
        if (map != null)
        {
            var pPos = map.WorldToCell(transform.position);
            if (map.GetTile(pPos) != null)
            {
                var bullet = Instantiate(particle, transform.position, Quaternion.identity);
            }
            map.SetTile(pPos, null);
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        eandestroy destroyObject = collision.gameObject.GetComponent(typeof(eandestroy))
                                  as eandestroy;

        if (collision.gameObject.tag != "Untagged" && destroyObject == null)
        {
            Destroy(gameObject);
        }
    }
}
