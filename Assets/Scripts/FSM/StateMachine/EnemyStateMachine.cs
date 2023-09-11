using UnityEngine;

public class EnemyStateMachine : State_Machine
{
    #region EXPOSED_FIELDS
    [SerializeField] private EnemyComponent enemy;
    #endregion

    #region PRIVATE_FIELDS
    private EnemyIdleState _idleState;
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

        _idleState = new EnemyIdleState(nameof(_idleState),this,enemy);
    }

    protected override State GetInitialState()
    {
        base.GetInitialState();
        return _idleState;
    }
}
