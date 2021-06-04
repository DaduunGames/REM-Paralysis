using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public ParticleSystem featherPuff;
    public AudioSource shotgunAudio;

    public float shootSpeed;
    private float shootSpeedMod;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {

            bool stunned = transform.root.GetComponent<PlayerMovement>().IsStunned;
            if (Input.GetButton("Fire1") && !stunned)
            {//when the left mouse button is clicked

                Shotgun();
                shotgunAudio.Play();

                //reset timer
                shootSpeedMod = transform.root.GetComponent<PlayerStats>().attackSpeedModifier;
                timer = shootSpeed;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void Shotgun()
    {
        GameObject Clone1;
        GameObject Clone2;
        GameObject Clone3;

        Clone1 = (Instantiate(bulletPrefab, transform.position, transform.rotation)) as GameObject;
        Clone2 = (Instantiate(bulletPrefab, transform.position, transform.rotation)) as GameObject;
        Clone3 = (Instantiate(bulletPrefab, transform.position, transform.rotation)) as GameObject;

        Clone1.transform.Rotate(0, 0, 30);
        Clone3.transform.Rotate(0, 0, -30);


        GetComponent<Animator>().Play("Shoot", 0, 0);
        featherPuff.Play();

    }
}
