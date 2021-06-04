using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSaver : MonoBehaviour
{
    Vector2 Direction;
    public float speed = 1;
    public float rotationSpeed = 0.2f;
    float rotationdirection = 1;

    float timer;
    float extraSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        Direction = new Vector2(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.Rotate(0, 0, speed * (rotationdirection + extraSpeed) * Time.deltaTime);
        
        

        Vector2 bounds = new Vector2(Screen.width, Screen.height);
        

        if (transform.position.x < 30)
        {
            Direction.x = 1;
            ToggleRotationDirection();
        }

        if (transform.position.y < 30)
        {
            Direction.y = 1;
            ToggleRotationDirection();
        }

        if (transform.position.x > Screen.width-30)
        {
            Direction.x = -1;
            ToggleRotationDirection();
        }

        if (transform.position.y > Screen.height-30)
        {
            Direction.y = -1;
            ToggleRotationDirection();
        }

        if(extraSpeed > 0)
        {
            extraSpeed -= Time.deltaTime;
        }
        
        transform.position = (Vector2)transform.position + (Direction * speed * Time.deltaTime);
    }

    void ToggleRotationDirection()
    {
        rotationdirection *= -1f;

        extraSpeed = 1 * rotationdirection;

    }
}
