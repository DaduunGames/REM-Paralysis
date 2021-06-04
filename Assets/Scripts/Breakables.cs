using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject[] LootTable;
    [Range(0,1)]
    public float lootChance;
    public GameObject breakParticles;
    

    private GameObject spawnLoot;

    [Range(0,1)]
    public float RandomBushChance;

    public AudioClip[] breakableSounds;
    public AudioSource breakablesAudio;

    private void Start()
    {
        spawnLoot = LootTable[Random.Range(0,LootTable.Length-1)];

        if (RandomBushChance >= Random.Range(0f,1f))
        {
            Destroy(gameObject);
        }

        //breakablesAudio = GetComponent<AudioSource>();

        //if (breakablesAudio == null)
        //{
        //    Debug.LogError("No AudioSource found");
        //}
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 9)
        {
            float rand = Random.Range(0f, 1f);
            //print( $"random{rand}. my chance: {lootChance}. bool: {lootChance >= rand}");
            
            
            Break(lootChance >= rand);
        }
    }


    private void Break(bool CanSpawnLoot)
    {
        Instantiate(breakParticles, transform.position, transform.rotation);

        if (CanSpawnLoot) 
        {
            Instantiate(spawnLoot, transform.position, transform.rotation);
        }

        Destroy(gameObject);
        PlayBreakingSound();
    }

    void PlayBreakingSound()
    {
        //Get an audioClip
        int n = Random.Range(1, breakableSounds.Length);
        breakablesAudio.clip = breakableSounds[n];
        //Play the sound once
        breakablesAudio.PlayOneShot(breakablesAudio.clip);
    }
}
