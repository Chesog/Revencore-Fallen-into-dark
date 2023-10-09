using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private GameObject _meleeEnemyPrefab;
    [SerializeField] private GameObject _distanceEnemyPrefab;

    [SerializeField] private float _meleeInterval = 3.5f;
    [SerializeField] private float _distanceInterval = 10f;

    [SerializeField] private EnemiesManager_T _enemiesManager;
    [SerializeField] private RoomManager _roomManager;
    [SerializeField] private Transform[] _room1SpawnPositions;
    [SerializeField] private Transform[] _room2SpawnPositions;

    #endregion

    #region PRIVATE_FIELDS

    private int _maxMeleeEnemies;
    private int _maxDistanceEnemies;
    private int _count = 0;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        _maxMeleeEnemies = _enemiesManager._room1NecessaryKills;
        _maxDistanceEnemies = _enemiesManager._room2NecessaryKills;
        
        if (_roomManager.GetCurrentRoom() == 1)
            StartCoroutine(spawnEnemy(_meleeInterval, _meleeEnemyPrefab, _maxMeleeEnemies, _room1SpawnPositions));
        else if(_roomManager.GetCurrentRoom() == 2)
            StartCoroutine(spawnEnemy(_distanceInterval, _distanceEnemyPrefab, _maxDistanceEnemies, _room2SpawnPositions));
        
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy, int maxEnemies, Transform[] spawners)
    {
        yield return new WaitForSeconds(interval);

        Transform randomSpawnPos = spawners[Random.Range(0, _room1SpawnPositions.Length)];

        GameObject newEnemy = Instantiate(enemy, randomSpawnPos.position, Quaternion.identity);
        _count++;

        if (_count < maxEnemies)
            StartCoroutine(spawnEnemy(interval, enemy, maxEnemies, spawners));
    }

    #endregion
}