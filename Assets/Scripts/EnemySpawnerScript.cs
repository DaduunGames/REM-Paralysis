using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject shover;
    public GameObject slinger;
    public GameObject wizard;
    Vector2 WhereToSpawn;
    public float spawnRate = 2f;
    float nextSpawn = 0.0f;
    public float spawnRadius;
    //bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void Spawn()
    {
        for (int i = 0; i < 2; i++)
        {
            //if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnRate;
                WhereToSpawn = Random.insideUnitCircle * spawnRadius;
                Instantiate(shover, WhereToSpawn, Quaternion.identity);
                WhereToSpawn = Random.insideUnitCircle * spawnRadius;
                Instantiate(slinger, WhereToSpawn, Quaternion.identity);
                WhereToSpawn = Random.insideUnitCircle * spawnRadius;
                Instantiate(wizard, WhereToSpawn, Quaternion.identity);
            }
        }
    }
}
