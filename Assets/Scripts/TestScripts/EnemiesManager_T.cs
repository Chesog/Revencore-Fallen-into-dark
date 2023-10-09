using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class EnemiesManager_T : MonoBehaviour
{
    #region EVENTS

    public static event Action NoEnemies;

    #endregion

    #region EXPOSED_FIELDS

    [SerializeField] private string _enemyTag = "Enemy";
    [SerializeField] private int _kills = 0;

    #endregion

    #region PUBLIC_FIELDS

    public int _room1NecessaryKills = 10;
    public int _room2NecessaryKills = 7;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        EnemyInputManager.OnEnemyDestroy += IncreaseKill;
        DistanceEnemy.Destroyed += IncreaseKill;
    }

    private void Update()
    {
        if (_kills >= _room1NecessaryKills)
        {
            NoEnemies?.Invoke();
        }
    }

    private void OnDestroy()
    {
        EnemyInputManager.OnEnemyDestroy -= IncreaseKill;
        DistanceEnemy.Destroyed -= IncreaseKill;
    }

    #endregion

    #region PRIVATE_METHODS

    private void IncreaseKill()
    {
        _kills++;
    }

    #endregion
}