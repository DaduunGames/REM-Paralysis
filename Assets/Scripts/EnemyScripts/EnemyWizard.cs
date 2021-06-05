using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWizard : enemyCore
{
    public GameObject summon;
    public float summonRange;

    public GameObject runAwayTarget;

    private bool isRunning;
    private float wandertimer;

    public float WalkingSpeed;
    public float Runningspeed;

    public float SummonCooldown;
    private float summontimer;
    public GameObject summonParticles;

    

    

    public override void MoveEnemy()
    {
        if (IsAgro)
        {
            aiPath.maxSpeed = Runningspeed;
            isRunning = true;

            runAwayTarget.transform.localPosition = (transform.position - Player.transform.position);
        }
        else
        {
            aiPath.maxSpeed = WalkingSpeed;
            isRunning = false;

            if (wandertimer <= 0)
            {
                wandertimer = Random.Range(1,2);

                runAwayTarget.transform.position = Random.insideUnitCircle.normalized;
            }
            else
            {
                wandertimer -= Time.deltaTime;
            }
        }

        destSetter.target = runAwayTarget.transform;

        if (!isRunning)
        {
            if (summontimer <= 0)
            {
                summontimer = SummonCooldown;
                
                Summon();

            }
            else
            {
                SummonColourTimer = summontimer/SummonCooldown;
                summontimer -= Time.deltaTime;
            }
        }
    }

    public void Summon()
    {
        
        Vector3 summonPos = Random.insideUnitCircle.normalized * summonRange;
        chest.KillGoal++;

        SummonColourTimer = 1f;

        GameObject summoned = Instantiate(summon, transform.position + summonPos, Quaternion.identity);
        summoned.GetComponent<enemyCore>().chest = chest;

        Instantiate(summonParticles, summoned.transform.position, summoned.transform.rotation);
    }
}
