using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    #region EVENTS

    public static event Action OnDestroyed;

    #endregion

    #region EXPOSED_FIELDS

    [SerializeField] private HealthComponent _characterHealthComponent;
    [SerializeField] private Player_Data_Source _playerData;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private float _bombRadius = 5f;
    [SerializeField] private float _bombCooldown = 2f;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _enemySpawnCooldown = 2f;
    [SerializeField] private float _enemySpawnRadius = 5f;
    [SerializeField] private float _pushForce = 10f;
    [SerializeField] private RoomManager _roomManager;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private GameObject _sliderParent;
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private CameraShaker _cameraShake;

    #endregion

    #region PRIVATE_FIELDS

    private float _lastBombDropTime = -2f;

    #endregion

    #region UNITY_CALLS

    private void OnEnable()
    {
        if (_characterHealthComponent == null)
        {
            _characterHealthComponent = GetComponent<HealthComponent>();
        }

        if (!_characterHealthComponent)
        {
            UnityEngine.Debug.LogError(message: $"{name}: (logError){nameof(_characterHealthComponent)} is null");
            enabled = false;
        }
        else
        {
            _characterHealthComponent.OnHealthChanged += UpdateHealthBar;
        }

        if (anim == null)
            anim = GetComponent<Animator>();

        _characterHealthComponent.OnInsufficient_Health += BoosDefeated;
    }

    private void Start()
    {
        _sliderParent.SetActive(false);
        _lastBombDropTime = -_bombCooldown;
        _healthBar.maxValue = _characterHealthComponent._maxHealth;
        _healthBar.value = _characterHealthComponent._maxHealth;
    }

    private void Update()
    {
        if (_roomManager.GetCurrentRoom() == 5)
        {
            _sliderParent.SetActive(true);

            if (Time.time >= _lastBombDropTime + _bombCooldown)
            {
                Attack();
                _lastBombDropTime = Time.time;
            }
        }
    }

    private void BoosDefeated()
    {
        _sliderParent.SetActive(false);
        OnDestroyed?.Invoke();
    }

    private void OnDestroy()
    {
        _characterHealthComponent.OnHealthChanged -= UpdateHealthBar;
        _characterHealthComponent.OnInsufficient_Health -= BoosDefeated;
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy, Transform playerTransform)
    {
        yield return new WaitForSeconds(interval);

        Vector3 randomPosition = Random.insideUnitSphere * _enemySpawnRadius;
        randomPosition += playerTransform.position;
        randomPosition.y = playerTransform.position.y;

        GameObject newEnemy = Instantiate(enemy, randomPosition, Quaternion.identity);
    }

    #endregion

    #region PRIVATE_METHODS

    private void Attack()
    {
        if (_characterHealthComponent._health > 2 * _characterHealthComponent._maxHealth / 3)
        {
            UnityEngine.Debug.Log("Boss stage: 1");
            DropBombs();
            anim.Play("Boss_Roar");
            _cameraShake.StartCameraShake();
            PushPlayer();
            AkSoundEngine.PostEvent("BossScream", gameObject);
        }
        else if (_characterHealthComponent._health > _characterHealthComponent._maxHealth / 3)
        {
            UnityEngine.Debug.Log("Boss stage: 2");
            anim.Play("Boss_Summoning");
            StartCoroutine(SpawnEnemy(_enemySpawnCooldown, _enemyPrefab, _playerData._player.transform));
            PushPlayer();
            AkSoundEngine.PostEvent("BossChannel", gameObject);
        }
        else if (_characterHealthComponent._health > 0)
        {
            UnityEngine.Debug.Log("Boss stage: 3");
            anim.Play("Boss_RoarSumm");
            _cameraShake.StartCameraShake();
            DropBombs();
            StartCoroutine(SpawnEnemy(_enemySpawnCooldown - (_enemySpawnCooldown * 0.5f), _enemyPrefab,
                _playerData._player.transform));
            PushPlayer();
            AkSoundEngine.PostEvent("BossScream", gameObject);
        }
        else
        {
            UnityEngine.Debug.Log("Boss Defeated");
            anim.Play("Boss_Death");
            AkSoundEngine.PostEvent("BossDeath", gameObject);
            Invoke("LoadWinScene", 1f);
            Destroy(gameObject, 1f);
        }
    }

    private void LoadWinScene()
    {
        SceneManager.LoadScene("WinVideo_Scene");
    }

    private void DropBombs()
    {
        Vector3 randomPosition = Random.insideUnitSphere * _bombRadius;
        randomPosition += _playerData._player.transform.position;
        randomPosition.y = _playerData._player.transform.position.y + 10;

        Instantiate(_bombPrefab, randomPosition, Quaternion.identity);
    }

    private void UpdateHealthBar()
    {
        _healthBar.value = _characterHealthComponent._health;
    }

    private void PushPlayer()
    {
        _playerRb.AddForce(Vector3.left * _pushForce, ForceMode.Impulse);
    }

    #endregion
}