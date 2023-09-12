using System;
using UnityEngine;

public class EnemyInputManager : MonoBehaviour
{
    [SerializeField] private EnemyComponent _enemy;
    public event Action OnEnemyMove;

    private void Update()
    {
        float distance = Vector3.Distance(_enemy.transform.position, _enemy.target.position);
        if (distance <= _enemy.lookRad || distance > _enemy.stopDistance)
            OnEnemyMove.Invoke();
    }
}