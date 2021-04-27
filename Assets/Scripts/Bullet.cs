using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;

    // Update is called once per frame
    void Update()
    {
        //transform.right is world right. Vector3 is relative right
        transform.Translate((Vector3.right * speed * Time.deltaTime));
    }
}
