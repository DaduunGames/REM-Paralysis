using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public ParticleSystem featherPuff;
    public AudioSource chargedAudio;

    private float totalCharge = 0f;
    private float totalChargeNeeded = 1f;
    private KeyCode chargedAndShootKey = KeyCode.Mouse0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(chargedAndShootKey))
        {
            totalCharge += Time.deltaTime;
        }
        if (Input.GetKeyUp(chargedAndShootKey))
        {
            Charged();
            chargedAudio.Play();
        }

    }

    void Charged()
    {
        if (totalCharge >= totalChargeNeeded)
        {
            GameObject Clone;

            Clone = (Instantiate(bulletPrefab, transform.position, transform.rotation)) as GameObject;
            Clone.transform.localScale = new Vector3(2, 2, 1);
        }

        totalCharge = 0f;
    }
}
