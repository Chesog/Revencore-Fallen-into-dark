using System;
using UnityEngine;

public class EnemyInputManager : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private EnemyComponent _enemy;
    [SerializeField] private string bulletTag = "Bullet";
    [SerializeField] private PlayerDamage_T player;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private float damageCooldown = 2f;
    [SerializeField] private float maxHealth = 100f;
    #endregion
    public event Action OnEnemyMove;
    public event Action OnEnemyAttack;
    public event Action OnEnemyHit;
    public static event Action OnEnemyDestroy;

    private void Update()
    {
        MovementCheck();
    }

    public void MovementCheck()
    {
        if (_enemy.target != null)
        {
            float  distance = Vector3.Distance(_enemy.transform.position, _enemy.target.position);
                
            if (distance <= _enemy.lookRad && distance > _enemy.stopDistance)
                OnEnemyMove?.Invoke();
            else if (distance <= _enemy.lookRad && distance <= _enemy.stopDistance)
                OnEnemyAttack?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(bulletTag))
        { 
            OnEnemyHit?.Invoke();
        }
    }

    public void OnTakeDamage()
    {
        OnEnemyHit?.Invoke();
    }

    private void OnDestroy()
    {
        OnEnemyDestroy?.Invoke();
    }
}