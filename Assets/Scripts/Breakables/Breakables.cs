using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject[] LootTable;
    [Range(0,1)]
    public float lootChance;
    public GameObject Coin;
    [Range(0, 1)]
    public float ExtraCoinChance;
    public GameObject breakParticles;
    

    private GameObject spawnLoot;

    [Range(0,1)]
    public float RandomBushChance;

    //public AudioClip[] breakableSounds;
    public AudioSource breakablesAudio;

    public bool isCrate = true;
    

    private void Start()
    {
        

        spawnLoot = LootTable[Random.Range(0,LootTable.Length-1)];

        if (RandomBushChance >= Random.Range(0f,1f))
        {
            Destroy(gameObject);
        }

        if (isCrate) 
        {
            breakablesAudio = FindObjectOfType<CrateBreakable>().GetComponent<AudioSource>();
        }
        else
        {
            breakablesAudio = FindObjectOfType<BushBreakable>().GetComponent<AudioSource>();
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


    public void Break(bool CanSpawnLoot)
    {
        Instantiate(breakParticles, transform.position, transform.rotation);

        if (CanSpawnLoot) 
        {
            Instantiate(spawnLoot, transform.position, transform.rotation);

            if (ExtraCoinChance >= Random.Range(0f,1f))
            {
                for (int i = Random.Range(1,3); i > 0; i--)
                {
                    GameObject spawned = Instantiate(Coin, transform.position, transform.rotation);
                    spawned.transform.position += (Vector3)Random.insideUnitCircle;
                }
            }
        }
        breakablesAudio.Play();
        Destroy(gameObject);
        
    }

    /*void PlayBreakingSound()
    {

        //breakablesAudio.clip = breakableSounds[Random.Range(0, breakableSounds.Length)];
        breakablesAudio.PlayOneShot(breakableSounds[Random.Range(0, breakableSounds.Length)]);
        
    }*/
}
