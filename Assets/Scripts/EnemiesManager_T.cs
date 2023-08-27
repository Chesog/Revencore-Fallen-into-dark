using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager_T : MonoBehaviour
{
    [SerializeField] private int enemiesToKill = 5;
    private int remainingEnemies;


    private void Awake()
    {
        Enemy_T.Destroyed += ReduceEnemy;
    }

    private void Start()
    {
        remainingEnemies = enemiesToKill;
    }

    private void Update()
    {
        if (remainingEnemies <= 0)
        {

        }
    }

    private void ReduceEnemy()
    {
        remainingEnemies--;
    }

    private void OnDestroy()
    {
        Enemy_T.Destroyed -= ReduceEnemy;
    }
}
