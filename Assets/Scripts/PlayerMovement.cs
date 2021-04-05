using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;

    public float walkSpeed;
    public float sprintSpeed;

    public Rigidbody2D rb;
    Vector2 movement;

    public float dashSpeed;
    public float dashFalloff;
    private float dashMovement = 1;

    public float cooldown;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        //Note:Lines take priority via the order they are in
        #region Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;

        }
        else
        {
            moveSpeed = walkSpeed;
        }
        #endregion

        #region Dash
        if (Input.GetKeyDown(KeyCode.Space) && timer <= 0)
        {
            dashMovement = dashSpeed;
            timer = cooldown;
        }

        if (dashMovement > 1)
        {
            moveSpeed = 1;
            dashMovement -= Time.deltaTime * dashFalloff;
        }
        else if(dashMovement < 1)
        {
            dashMovement = 1;
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        #endregion


        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * dashMovement * Time.fixedDeltaTime);
    }
}
