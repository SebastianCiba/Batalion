﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public bool DebugStopSpawners;

    [SerializeField]
    private Enemy[] EnemyPrefabs;

    [SerializeField]
    private Transform EnemyHolder;

    private Dictionary<int, Enemy> enemyPrefabsDict = new Dictionary<int, Enemy>();
    private bool isInitialized;

    private List<EnemySpawner> spawners = new List<EnemySpawner>();

    private void Update()
    {
#if UNITY_EDITOR
        if (DebugStopSpawners)
        {
            StopAllSpawners();
            DebugStopSpawners = false;
        }
#endif
    }

    public Enemy InstantiateEnemyWithId(int id, Vector3 position, Quaternion rotation)
    {
        if (!isInitialized)
        {
            InitializeDict();
        }
        return Instantiate(enemyPrefabsDict[id], position, rotation, EnemyHolder);
    }

    public void StopAllSpawners()
    {
        for (int i = 0; i < spawners.Count; i++)
        {
            spawners[i].SetActive(false);
        }
    }

    public void RegisterSpawner(EnemySpawner spawner)
    {
        spawners.Add(spawner);
    }

    private void InitializeDict()
    {
        for (int i = 0; i < EnemyPrefabs.Length; i++)
        {
            int id = EnemyPrefabs[i].Id;
            if (!enemyPrefabsDict.ContainsKey(id))
            {
                enemyPrefabsDict.Add(id, EnemyPrefabs[i]);
            }
            else
            {
                Debug.LogError("Array has prefabs with duplicate enemy Ids! You need to change enemy id to unique");
                return;
            }
        }
        isInitialized = true;
    }
}