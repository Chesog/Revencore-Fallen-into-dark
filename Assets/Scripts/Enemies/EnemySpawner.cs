using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private GameObject _meleeEnemyPrefab;
    [SerializeField] private GameObject _distanceEnemyPrefab;

    [SerializeField] private float _meleeInterval = 3.5f;
    [SerializeField] private float _distanceInterval = 10f;
    [SerializeField] private int _maxDistanceEnemies = 5;

    [SerializeField] private EnemiesManager_T _enemiesManager;
    [SerializeField] private Transform[] _spawnPositions;

    #endregion

    #region PRIVATE_FIELDS

    private int _maxMeleeEnemies;
    private int _count = 0;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        _maxMeleeEnemies = _enemiesManager._necessaryKills;
        StartCoroutine(spawnEnemy(_meleeInterval, _meleeEnemyPrefab, _maxMeleeEnemies));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy, int maxEnemies)
    {
        yield return new WaitForSeconds(interval);
        
        Transform randomSpawnPos = _spawnPositions[Random.Range(0, _spawnPositions.Length)];
        
        GameObject newEnemy = Instantiate(enemy, randomSpawnPos.position, Quaternion.identity);
        _count++;
        
        if (_count < maxEnemies)
            StartCoroutine(spawnEnemy(interval, enemy, maxEnemies));
    }

    #endregion
}