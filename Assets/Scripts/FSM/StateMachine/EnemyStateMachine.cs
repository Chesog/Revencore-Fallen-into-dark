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

        _idleState = new EnemyIdleState(nameof(_idleState),this,enemy);
        _moveState = new EnemyMoveState(nameof(_idleState),this,enemy);
        
        base.OnEnable();
    }

    private void OnEnemyMove()
    {
        SetState(_moveState);
    }

    protected override State GetInitialState()
    {
        base.GetInitialState();
        return _idleState;
    }
}
