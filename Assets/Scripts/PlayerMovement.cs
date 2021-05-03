using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 movement;

    public ParticleSystem dust;

    public Animator anim_Player;
    public Animator anim_Pillow;

    public GameObject pillow;

    #region Walking Variables
    private float moveSpeed = 5f;
    public float walkSpeed;
    public float sprintModifier;
    #endregion
    
    #region Dash Variables
    public float dashSpeed;
    private float dashMovement = 1;

    public float cooldown;
    private float timer;
    #endregion

    #region Bullet
    //public GameObject bulletToRight, bulletToLeft;
    //Vector2 bulletPos;
    //public float fireRate = 0.5f;
    //float nextFire = 0.0f;
    #endregion

    Camera cam;

    PlayerStats pStats;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        pStats = GetComponent<PlayerStats>();
        
    }


    // Update is called once per frame
    void Update()
    {
        //Note:Lines take priority via the order they are in
        #region Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed * sprintModifier * pStats.movementSpeedModifier;

        }
        else
        {
            moveSpeed = walkSpeed * pStats.movementSpeedModifier;
        }
        #endregion

        #region Dash
        if (Input.GetKeyDown(KeyCode.Space) && timer <= 0)
        {
            dashMovement = dashSpeed * pStats.dashDistanceModifier;
            timer = cooldown * pStats.dashCooldownModifier;
            CreateDust();
        }

        if (dashMovement > 1)
        {
            moveSpeed = 1;
            dashMovement -= Time.deltaTime * (dashMovement * 10);
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

        //if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire)
        //{
        //    nextFire = Time.time + fireRate;
        //    fire();
        //}

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

    //void fire()
    //{
    //    bulletPos = transform.position;
  
    //}
}
