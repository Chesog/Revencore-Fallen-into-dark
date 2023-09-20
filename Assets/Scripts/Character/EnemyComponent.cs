using System;
using System.Collections;
using UnityEngine;

public class EnemyComponent : CharacterComponent
{
    public float lookRad = 20f;
    public float stopDistance = 5f;
    public float timeBetweenAttacks = 6.0f;
    public float destroyTime;
    public float destroyTimer;
    public bool ready_To_Attack;
    public bool deathLoop;
    public bool isHit;
    public Transform target;
    public Transform bulletSpawn;
    public GameObject floatingTextPrefab;
    public Transform floatingTextSpawn;
    public FloatingTextHandlerer floatingTextHandleer;

    public Player_Data_Source player_Source;

    public GameObject bulletPrefab;

    public event Action<Vector2> OnEnemyMove;
    public event Action OnEnemyAttack;
    public event Action OnEnemyHit;
    public event Action OnEnemyDeath;
    

    private void OnEnable()
    {
        character_Health_Component._health = character_Health_Component._maxHealth;
        initialSpeed = speed;

        ready_To_Attack = true;
        
        if (target == null)
            if (player_Source._player)
            {
                target = player_Source._player.transform;
            }
        if (!target)
        {
            StartCoroutine(SearchForPlayerEverySecond());
            //Debug.LogError(message: $"{name}: (logError){nameof(target)} is null");
        }

        deathLoop = false;
    }

    private IEnumerator SearchForPlayerEverySecond()
    {
        while (!target)
        {
            yield return new WaitForSeconds(1);
            target = player_Source._player.transform;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
    }

    /// <summary>
    /// Make The Enemy Take Damage Based On The Damage Param
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        //character_Health_Component.DecreaseHealth(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRad);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}