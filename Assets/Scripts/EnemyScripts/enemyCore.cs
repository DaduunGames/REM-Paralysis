using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

//[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AIPath))]

public class enemyCore : MonoBehaviour
{
    #region variables

    public Sprite[] directionalBodies;
    public Sprite[] agroDirectionalBodies;
    public SpriteRenderer SP;

    private Animator anim;
    public AIPath aiPath { get; private set; }
    public AIDestinationSetter destSetter { get; private set; }

    public GameObject Player;

    //==========
    public bool IsAgro { get; private set; }
    public bool inAttackRange { get; private set; }
    public bool showRadiuses;

    public float health;
    public float AgroRadius = 3f;
    public float MustHavebeenthewind = 5f;
    public float attackRange = 1.5f;

    public int attackDamage;
    public int DamageRandom;

    public float pushforce = 30;
    public float stuntime = 0.5f;

    public float hitCooldown = 2;
    private float cooldowntimer;

    private Vector3 debugVector;


    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        aiPath = GetComponent<AIPath>();
        destSetter = GetComponent<AIDestinationSetter>();
        Player = FindObjectOfType<PlayerMovement>().gameObject;
       

        destSetter.target = null;
        
    }

    private void Update()
    {
        #region attack trigger
        if (Vector2.Distance(Player.transform.position, transform.position) <= attackRange)
        {
            inAttackRange = true;

            if (cooldowntimer <= 0)
            {
                print("attacking!!!!!!");
                Attack();
                cooldowntimer = hitCooldown;
            }

            else
            {
                cooldowntimer -= Time.deltaTime;
            }
        }
        else
        {
            inAttackRange = false;
        }
        #endregion

        #region agro once in radius
        if (Vector2.Distance(transform.position, Player.transform.position) < AgroRadius)
        {
            IsAgro = true;
            
        }
        if(Vector2.Distance(transform.position, Player.transform.position) > MustHavebeenthewind)
        {
            IsAgro = false;
           
        }
        #endregion

        MoveEnemy();

        #region update Visuals

        if (aiPath.velocity != Vector3.zero)    //When AI is Moving
        {
            anim.SetBool("Walking", true);
        }
        else                                    //Whenn AI is Still              
        {
            anim.SetBool("Walking", false);
        }

        int x = Mathf.RoundToInt(aiPath.velocity.normalized.x);
        int y = Mathf.RoundToInt(aiPath.velocity.normalized.y);

        switch (new Vector2(x, y))
        {
            case Vector2 v when v.Equals(new Vector2(0, 1)):     //U
                ChangeSprite(0);
                break;

            case Vector2 v when v.Equals(new Vector2(1, 1)):    //UR
                ChangeSprite(1);
                break;

            case Vector2 v when v.Equals(new Vector2(1, 0)):    //R
                ChangeSprite(2);
                break;

            case Vector2 v when v.Equals(new Vector2(1, -1)):   //DR
                ChangeSprite(3);
                break;

            case Vector2 v when v.Equals(new Vector2(0, -1)):   //D
                ChangeSprite(4);
                break;

            case Vector2 v when v.Equals(new Vector2(-1, -1)):  //DL
                ChangeSprite(5);
                break;

            case Vector2 v when v.Equals(new Vector2(-1, 0)):   //L
                ChangeSprite(6);
                break;

            case Vector2 v when v.Equals(new Vector2(-1, 1)):   //UL
                ChangeSprite(7);
                break;
        }
        #endregion
    }

    public virtual void MoveEnemy()
    {
        if (IsAgro && !inAttackRange)
        {
            destSetter.target = Player.transform;
        }
        else
        {
            aiPath.SetPath(null);
            destSetter.target = null;
        }
    }

    public virtual void Attack() 
    {
        DamagePlayer();
        StunPlayer(stuntime);

        Vector2 force = Player.transform.position - transform.position;
        force.Normalize();
        Player.GetComponent<Rigidbody2D>().AddForce(force * pushforce, ForceMode2D.Impulse);
    }

    public void DamagePlayer()
    {
        int randDMG = Mathf.RoundToInt(attackDamage + Random.Range(-DamageRandom, DamageRandom));
        int dmg = (int)Mathf.Clamp(randDMG, 1, 999);

        Player.GetComponent<PlayerStats>().health -= dmg;
    }

    public void StunPlayer(float time)
    {
        Player.GetComponent<PlayerMovement>().stunTimer = time;
    }

    private void ChangeSprite(int direction)
    {
        Sprite normalSprite = directionalBodies[direction];
        Sprite agroSrite;

        if (IsAgro)
        {
            if (agroDirectionalBodies.Length != 0)
            {
                agroSrite = agroDirectionalBodies[direction];
                SP.sprite = agroSrite;
            }
            else
            {
                SP.sprite = normalSprite;
            }
        }
        else
        {
            SP.sprite = normalSprite;
        }
    }


    private void OnDrawGizmos()
    {
        if (showRadiuses)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, AgroRadius);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, MustHavebeenthewind);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }


        //Gizmos.color = Color.cyan;
        //Gizmos.DrawIcon(transform.position + debugVector, "", true);
    }
}
