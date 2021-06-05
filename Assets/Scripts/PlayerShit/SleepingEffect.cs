using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingEffect : MonoBehaviour
{
    public GameObject z;
    public float timeBetween = 0.5f;
    public int numOfParticles = 8;

    private void Start()
    {
        StartCoroutine(SpawnParticle());
    }

    IEnumerator SpawnParticle()
    {
        while (numOfParticles > 0)
        {
            GameObject zed = Instantiate(z, transform.position, transform.rotation);
            //zed.transform.parent = this.transform;
            zed.transform.SetParent(this.transform);

            yield return new WaitForSeconds(timeBetween);
            numOfParticles--;
        }

        yield return null;
    }
}
