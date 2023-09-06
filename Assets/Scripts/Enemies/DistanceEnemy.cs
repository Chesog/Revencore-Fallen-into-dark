using System.Collections;
using System;
using UnityEngine;

public class DistanceEnemy : MonoBehaviour
{
    #region EVENTS
    public static event Action Destroyed;
    #endregion

    #region EXPOSED_FIELDS
    [SerializeField] private string _bulletTag = "Bullet";
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private GameObject _floatingTextPrefab;
    [SerializeField] private Transform _floatingTextSpawn;
    [SerializeField] private PlayerDamage_T _player;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _attackDistance = 2f;
    private float _currentHealth;
    private float _distanceToPlayer;
    private bool _isAttacking = false;
    #endregion

    #region UNITY_CALLS
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (_distanceToPlayer > _attackDistance)
        {
            Vector3 targetPosition = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }
        else if (_distanceToPlayer <= _attackDistance && Mathf.Approximately(transform.position.z, _player.transform.position.z))
        {
            Vector3 direction = _player.transform.position - transform.position;
            direction.y = 0f;

            if (direction.x > 0)
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            else
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
             
            Quaternion rot = Quaternion.LookRotation(-transform.right);

            if (!_isAttacking)
            {
                GameObject projectile = Instantiate(_projectilePrefab, _shootingPoint.position, rot);

                _isAttacking = true;
                StartCoroutine(ResetAttack());
            }

        }
        else if(!Mathf.Approximately(transform.position.x, _player.transform.position.x))
        {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }

        if (!IsAlive())
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Debug.Log("Mataste un enemigo a distancia!");
        Destroyed?.Invoke();
    }

    #endregion
    
    #region PRIVATE_METHODS
    /// <summary>
    /// Resets the enemy's attack state after a certain cooldown, allowing it to attack again.
    /// </summary>
    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _isAttacking = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_bulletTag))
        {
            _currentHealth -= 20;

            if (_floatingTextPrefab)
                ShowFloatingText();

        }
    }

    private void ShowFloatingText()
    {
        var go = Instantiate(_floatingTextPrefab, _floatingTextSpawn.position, Quaternion.identity, transform);
        go.SetActive(true);
        go.GetComponent<TextMesh>().text = _currentHealth.ToString();
    }

    private bool IsAlive()
    {
        return _currentHealth > 0;
    }
    

    #endregion
}
