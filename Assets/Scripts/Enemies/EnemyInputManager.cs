using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyInputManager : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private EnemyComponent _enemy;
    [SerializeField] private string bulletTag = "Bullet";
    [SerializeField] private PlayerDamage_T player;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private float damageCooldown = 2f;
    [SerializeField] private float maxHealth = 100f;
    private bool isPaused;
    #endregion
    public event Action OnEnemyMove;
    public event Action OnEnemyAttack;
    public event Action OnEnemyHit;

    public event Action OnGamePause;
    public event Action OnGameUnPause;
    public static event Action OnEnemyDestroy;

    private void OnEnable()
    {
        roomManager = FindObjectOfType<RoomManager>();
        RoomManager.OnPause += OnPause;
        RoomManager.OnUnPause += OnUnPause;
        EnemiesManager.OnGamePause += OnPause;
    }
    
    private void OnUnPause()
    {
        isPaused = false;
        OnGameUnPause?.Invoke();
    }

    private void OnPause()
    {
        isPaused = true;
        OnGamePause?.Invoke();
    }

    private void Update()
    {
        if (!isPaused) {MovementCheck(); }
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
        RoomManager.OnPause -= OnPause;
        RoomManager.OnUnPause -= OnUnPause;
        EnemiesManager.OnGamePause -= OnPause;
    }
}