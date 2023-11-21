using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : State_Machine
{
    #region EXPOSED_FIELDS

    [SerializeField] private EnemyComponent enemy;
    [SerializeField] private EnemyInputManager enemyInput;
    [SerializeField] private float _attackCooldown;

    #endregion

    #region PRIVATE_FIELDS

    private EnemyIdleState _idleState;
    private EnemyMoveState _moveState;
    private EnemyHitState _hitState;
    private EnemyAttackState _attackState;

    #endregion

    private void OnEnable()
    {
        if (enemy == null)
            enemy = GetComponent<EnemyComponent>();
        if (!enemy)
        {
            Debug.LogError(message: $"{name}: (logError){nameof(enemy)} is null");
            enabled = false;
        }


        _idleState = new EnemyIdleState(nameof(_idleState), this, enemy);
        _moveState = new EnemyMoveState(nameof(_moveState), this, enemy);
        _hitState = new EnemyHitState(nameof(_hitState), this, enemy);
        _attackState = new EnemyAttackState(nameof(_attackState), this, enemy);

        enemyInput.OnEnemyMove += OnEnemyMove;
        enemyInput.OnEnemyAttack += OnEnemyAttack;
        enemyInput.OnEnemyHit += OnEnemyHit;
        enemyInput.OnGamePause += OnGamePause;
        EnemiesManager.OnGamePause += OnGamePause;

        enemy.character_Health_Component.OnDecrease_Health += OnEnemyHit;
        enemy.character_Health_Component.OnInsufficient_Health += OnInsuficientHealth;
        _attackState.OnEnemyShoot += OnEnemyShoot;
        _attackState.OnEnemyMeleeHit += OnEnemyMeleeHit;

        enemy.isHit = false;
        enemy.IsAttacking = false;

        base.OnEnable();
    }

    private void OnGamePause()
    {
        SetState(_idleState);
    }

    private void OnEnemyMeleeHit()
    {
        enemy.IsAttacking = true;
        StartCoroutine(ResetAttack());
    }

    private void OnEnemyShoot()
    {
        Quaternion rot = Quaternion.LookRotation(-transform.right);
        GameObject projectile = Instantiate(enemy.bulletPrefab, enemy.bulletSpawn.position, rot);

        enemy.IsAttacking = true;
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(_attackCooldown);
        enemy.IsAttacking = false;
    }

    private void OnEnemyHit()
    {
        if (enemy.character_Health_Component._health >= 0)
            SetState(_hitState);
    }

    private void OnInsuficientHealth()
    {
        if (enemy.character_Health_Component._health <= 0)
        {
            if (enemy.IsRangedEnemy)
            {
                AkSoundEngine.PostEvent("EnemySpitterDeath", gameObject); // sfx
            }
            else
            {
                AkSoundEngine.PostEvent("EnemyMinionDeath", gameObject); // sfx
            }

            Destroy(this.gameObject);
        }

        
    }

    private void OnEnemyAttack()
    {
        if (!enemy.isHit)
            SetState(_attackState);
    }

    private void OnEnemyMove()
    {
        if (!enemy.isHit)
            SetState(_moveState);
    }

    protected override State GetInitialState()
    {
        base.GetInitialState();
        return _idleState;
    }

    private void OnDestroy()
    {
        enemyInput.OnEnemyMove -= OnEnemyMove;
        enemyInput.OnEnemyAttack -= OnEnemyAttack;
        enemyInput.OnEnemyHit -= OnEnemyHit;
        enemyInput.OnGamePause -= OnGamePause;
        EnemiesManager.OnGamePause -= OnGamePause;
    }

    private void OnDrawGizmos()
    {
        
        if (!enemy.IsAttacking)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(enemy.sphereCenter, enemy.stopDistance);
        }
        else
            Gizmos.color = Color.black;
    }
}