using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager_T : MonoBehaviour
{
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
            //Camera to the next stage
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
