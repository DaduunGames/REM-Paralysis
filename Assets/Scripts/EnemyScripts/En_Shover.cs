using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_Shover : MonoBehaviour
{
    public float attackRange = 1.5f;
    public bool ShowAttackRange;

    public enemyCore eCore;

    private void Update()
    {
        if (Vector2.Distance(eCore.Player.transform.position,transform.position) <= attackRange)
        {
            Attack();
        }
    }

    private void Attack()
    {

    }

    private void OnDrawGizmos()
    {
        if (ShowAttackRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
