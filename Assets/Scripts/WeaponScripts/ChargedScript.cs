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

    public Gradient ChargeOverTime;
    public SpriteRenderer spr;

    // Update is called once per frame
    void Update()
    {
        spr.color = ChargeOverTime.Evaluate(totalCharge / totalChargeNeeded);

        if (Input.GetKey(chargedAndShootKey) && !PauseMenu.GameIsPaused)
        {
            totalCharge += Time.deltaTime;
        }
        if (Input.GetKeyUp(chargedAndShootKey) && !PauseMenu.GameIsPaused)
        {
            Charged();
            //moved teh audio into the Charged() function so that it only plays when you're successful
            //chargedAudio.Play();
        }

    }

    void Charged()
    {
        if (totalCharge >= totalChargeNeeded && !PauseMenu.GameIsPaused)
        {
            chargedAudio.Play();

            GameObject Clone;

            Clone = (Instantiate(bulletPrefab, transform.position, transform.rotation)) as GameObject;
            Clone.transform.localScale = new Vector3(2, 2, 1);
        }

        totalCharge = 0f;
    }
}
