using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlinger : enemyCore
{
    public GameObject Bullet;

    public GameObject runAwayTarget;

    private bool isRunning;
    private float wandertimer;

    public float WalkingSpeed;
    public float Runningspeed;

    public float SummonCooldown;
    private float shootTimer;

    public float RunawayRadius;

   

    public override void MoveEnemy()
    {
        if (IsAgro)
        {
            float dist = Vector2.Distance(transform.position, Player.transform.position);
            if (dist < MustHavebeenthewind && dist > RunawayRadius )
            {
                
                isRunning = false;
                runAwayTarget.transform.position = Player.transform.position;

                destSetter.target = runAwayTarget.transform;
                aiPath.maxSpeed = WalkingSpeed;
            }
            else
            {
               
                isRunning = true;

                runAwayTarget.transform.localPosition = (transform.position - Player.transform.position);

                destSetter.target = runAwayTarget.transform;
                aiPath.maxSpeed = Runningspeed;
            }

            if (!isRunning)
            {
                if (shootTimer <= 0)
                {
                    shootTimer = SummonCooldown;

                    Shoot();

                }
                else
                {
                    shootTimer -= Time.deltaTime;
                }
            }

        }
        else
        {
            
            isRunning = false;

            if (wandertimer <= 0)
            {
                wandertimer = Random.Range(1, 2);

                runAwayTarget.transform.position = Random.insideUnitCircle.normalized;
            }
            else
            {
                wandertimer -= Time.deltaTime;
            }

            destSetter.target = runAwayTarget.transform;
            aiPath.maxSpeed = WalkingSpeed;
        }

    }

    public void Shoot()
    {
        if (JustSpawned <= 0)
        {
            GameObject bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            Vector2 triangle = Player.transform.position - transform.position;
            float z = Mathf.Atan2(triangle.y, triangle.x) * Mathf.Rad2Deg;
            bullet.transform.Rotate(new Vector3(0, 0, z));
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

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, RunawayRadius);
        }
    }

}
