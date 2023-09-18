using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemiesManager_T : MonoBehaviour
{
    #region EVENTS
    public static event Action NoEnemies;
    #endregion

    #region EXPOSED_FIELDS
    [SerializeField] private int enemiesToKill = 5;
    private int remainingEnemies;
    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        Enemy_T.Destroyed += ReduceEnemy;
        DistanceEnemy.Destroyed += ReduceEnemy;
    }

    private void Start()
    {
        remainingEnemies = enemiesToKill;
    }

    private void Update()
    {
        if (remainingEnemies <= 0)
        {
            NoEnemies?.Invoke();
        }
    }
    private void OnDestroy()
    {
        Enemy_T.Destroyed -= ReduceEnemy;
        DistanceEnemy.Destroyed -= ReduceEnemy;
    }
    #endregion

    #region PRIVATE_METHODS
    private void ReduceEnemy()
    {
        remainingEnemies--;
    }
    #endregion

}
