﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestSpawner : MonoBehaviour
{
    public int KillGoal;
    public int currentKills = 0;

    public GameObject Chest;

    public EnemySpawnerScript[] Spawners;

    private void Start()
    {
        Chest.SetActive(false);

        foreach(EnemySpawnerScript spawner in Spawners)
        {
            KillGoal++;
            spawner.chest = this;
        }
    }

    void Update()
    {
        if (currentKills >= KillGoal && KillGoal != 0)
        {
            Chest.SetActive(true);
        }
    }
}
