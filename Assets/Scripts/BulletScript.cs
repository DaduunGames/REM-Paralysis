using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public GameObject bulletPrefab;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {//when the left mouse button is clicked

            print("1");//print a message to act as a debug

            FireBullet();//look for and use the fire bullet operation

        }

    }

    void FireBullet()
    {
        //Clone of the bullet
        GameObject Clone;

        //spawning the bullet at position
        Clone = (Instantiate(bulletPrefab, transform.position, transform.rotation)) as GameObject;
        Debug.Log("Bullet is found");


    }



}

