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

    public Animator anim_Player;
    public Animator anim_Pillow;

    public GameObject pillow;

    public ParticleSystem dust;
    

    Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }


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
            CreateDust();
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


        RotatePillow();


        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();


        if(movement != Vector2.zero)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            anim_Player.Play("Walk");
            anim_Player.SetFloat("x", x);
            anim_Player.SetFloat("y", y);

            
        }
        else
        {
            anim_Player.Play("Idle");
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * dashMovement * Time.fixedDeltaTime);
    }

    void RotatePillow()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vDif = (Vector2)transform.position - mousePos;


        float angle = Mathf.Atan2(vDif.y, vDif.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        pillow.transform.rotation = q;

        vDif.Normalize();

        anim_Pillow.SetFloat("x", Mathf.RoundToInt(vDif.x));
        anim_Pillow.SetFloat("y", Mathf.RoundToInt(vDif.y));
    }

    void CreateDust()
    {
        dust.Play();
    }
}
