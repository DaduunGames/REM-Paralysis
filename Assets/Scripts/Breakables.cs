using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject particles;
    public GameObject spawnLoot;



    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Yes");

        if (col.gameObject.layer == 9)
        {
            Break();
            Debug.Log("BeatYaYeet");
        }
    }


    private void Break()
    {
        Instantiate(particles, transform.position, transform.rotation);
        Instantiate(spawnLoot, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
