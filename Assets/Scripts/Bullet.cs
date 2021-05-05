using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    public float timer = 10f;

    // Update is called once per frame
    void Update()
    {
        //transform.right is world right. Vector3 is relative right
        transform.Translate((Vector3.right * speed * Time.deltaTime));

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }


}
