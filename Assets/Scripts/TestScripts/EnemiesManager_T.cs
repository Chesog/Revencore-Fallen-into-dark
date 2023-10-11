using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class EnemiesManager_T : MonoBehaviour
{
    #region EVENTS

    public static event Action OnNoEnemies;

    #endregion

    #region EXPOSED_FIELDS

    [SerializeField] private string _enemyTag = "Enemy";
    [SerializeField] private int _kills = 0;

    #endregion

    #region PRIVATE_FIELDS

    private int _currentRoom = 1;

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
        RoomManager.OnNewRoom += ResetKill;
    }

    private void Update()
    {
        if (_currentRoom == 1 && _kills >= _room1NecessaryKills)
        {
            _currentRoom++;
            OnNoEnemies?.Invoke();
        }
        else if (_currentRoom == 2 && _kills >= _room2NecessaryKills)
        {
            OnNoEnemies?.Invoke();
        }
    }

    private void OnDestroy()
    {
        EnemyInputManager.OnEnemyDestroy -= IncreaseKill;
        DistanceEnemy.Destroyed -= IncreaseKill;
        RoomManager.OnNewRoom -= ResetKill;
    }

    #endregion

    #region PRIVATE_METHODS

    private void IncreaseKill()
    {
        _kills++;
    }

    #endregion

    #region PUBLIC_METHODS

    public void ResetKill()
    {
        _kills = 0;
    }

    #endregion
}