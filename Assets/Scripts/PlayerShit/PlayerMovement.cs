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
    public bool isDashing;

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

    public bool IsStunned;
    public float stunTimer;
    public Color stunedColour;
    private SpriteRenderer[] bodyParts;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        pStats = GetComponent<PlayerStats>();

        bodyParts = GetComponentsInChildren<SpriteRenderer>();
        
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
            isDashing = true;
        }
        else if(dashMovement < 1)
        {
            dashMovement = 1;
            isDashing = false;
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        #endregion

        if (!IsStunned)
        {
            RotatePillow();
            foreach (SpriteRenderer spr in bodyParts)
            {
                spr.color = Color.white;
            }
        }
        else
        {
            foreach (SpriteRenderer spr in bodyParts)
            {
                spr.color = stunedColour;
            }
        }

        if (stunTimer > 0) //currently stunned
        {
            IsStunned = true;
            stunTimer -= Time.deltaTime;
        }
        else
        {
            IsStunned = false;
        }
        


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
        if (!IsStunned)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * dashMovement * Time.fixedDeltaTime);
        }
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "EBullet")
        {
            pStats.health = Mathf.Clamp(pStats.health - 1, 0, pStats.maxHealth);
            stunTimer = 0.3f;

            Vector2 force = col.transform.position - transform.position;
            force.Normalize();
            force *= -1;
            GetComponent<Rigidbody2D>().AddForce(force * 4, ForceMode2D.Impulse);

            Destroy(col.gameObject);
        }
    }
}
