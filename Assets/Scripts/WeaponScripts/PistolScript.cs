using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{

    public GameObject bulletPrefab;
    public ParticleSystem featherPuff;
    public AudioSource pistolAudio;

    public float shootSpeed;
    private float shootSpeedMod;
    private float timer;

    


    void Update()
    {
        if (timer <= 0)
        {

            bool stunned = transform.root.GetComponent<PlayerMovement>().IsStunned;
            if (Input.GetButton("Fire1") && !stunned && !PauseMenu.GameIsPaused)
            {//when the left mouse button is clicked

                Pistol();
                pistolAudio.Play();

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

