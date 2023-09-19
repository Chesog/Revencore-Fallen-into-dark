using System;
using UnityEngine;

public class EnemyStateMachine : State_Machine
{
    #region EXPOSED_FIELDS
    [SerializeField] private EnemyComponent enemy;
    [SerializeField] private EnemyInputManager enemyInput;
    #endregion

    #region PRIVATE_FIELDS
    private EnemyIdleState _idleState;
    private EnemyMoveState _moveState;
    private EnemyHitState _hitState;
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

        enemyInput.OnEnemyMove += OnEnemyMove;
        enemyInput.OnEnemyAttack += OnEnemyAttack;
        enemyInput.OnEnemyHit += OnEnemyHit;

        _idleState = new EnemyIdleState(nameof(_idleState),this,enemy);
        _moveState = new EnemyMoveState(nameof(_moveState),this,enemy);
        _hitState = new EnemyHitState(nameof(_hitState),this,enemy);

        enemy.isHit = false;
        
        base.OnEnable();
    }

    private void OnEnemyHit()
    {
        if (enemy.character_Health_Component._health <= 0)
            Destroy(this.gameObject);
            
        SetState(_hitState);
    }

    private void OnEnemyAttack()
    {
        if (!enemy.isHit)
        SetState(_idleState);
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
    }
}
