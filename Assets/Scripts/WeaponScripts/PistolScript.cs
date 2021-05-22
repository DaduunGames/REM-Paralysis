using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{

    public GameObject bulletPrefab;
    public ParticleSystem featherPuff;

    public float shootSpeed;
    private float shootSpeedMod;
    private float timer;

    private float totalCharge = 0f;
    private float totalChargeNeeded = 3f;
    private KeyCode chargedAndShootKey = KeyCode.Mouse0;



    void Update()
    {
        if (timer <= 0)
        {

            bool stunned = transform.root.GetComponent<PlayerMovement>().IsStunned;
            if (Input.GetButton("Fire1") && !stunned)
            {//when the left mouse button is clicked

                Pistol();

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

    void Pistol()
    {
        //Clone of the bullet
        GameObject Clone;

        
        //spawning the bullet at position
        Clone = (Instantiate(bulletPrefab, transform.position, transform.rotation)) as GameObject;
        //Debug.Log("Bullet is found");

       

        GetComponent<Animator>().Play("Shoot",0,0);
        featherPuff.Play();
    }



}

