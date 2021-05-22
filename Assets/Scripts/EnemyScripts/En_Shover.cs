using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Shover : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float RangeRandom = 0.2f;
    private float instanceRange = 0;
    public bool ShowAttackRange;

    public int attackDamage;
    public int DamageRandom = 1;

    public float pushforce = 1;
    public float stuntime = 1;

    public float hitCooldown = 2;
    private float cooldowntimer;

    public enemyCore eCore;

    private bool isAttacking;

    private void Start()
    {
        instanceRange = attackRange + Random.Range(-RangeRandom, RangeRandom);
    }

    private void Update()
    {
        if (Vector2.Distance(eCore.Player.transform.position, transform.position) <= instanceRange)
        {
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
    }

    private void Attack()
    {
        eCore.DamagePlayer(attackDamage, DamageRandom);
        eCore.StunPlayer(stuntime);

        Vector2 force = eCore.Player.transform.position - transform.position;
        force.Normalize();
        eCore.Player.GetComponent<Rigidbody2D>().AddForce(force * pushforce, ForceMode2D.Impulse);

    }

    private void OnDrawGizmos()
    {
        if (ShowAttackRange)
        {
            Gizmos.color = Color.red;
            if(instanceRange != 0)
            {
                Gizmos.DrawWireSphere(transform.position, instanceRange);
            }
            else
            {
                Gizmos.DrawWireSphere(transform.position, attackRange);
            }
        }
    }
}
