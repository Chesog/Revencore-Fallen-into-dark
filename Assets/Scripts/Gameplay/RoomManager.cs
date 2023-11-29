using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    #region EVENTS

    public static event Action OnNewRoom;
    public static event Action OnPause;
    public static event Action OnUnPause;

    #endregion

    [System.Serializable]
    private struct Room
    {
        public int roomNumber;
        public GameObject wall;
        public bool _canMoveForward;
    }

    #region EXPOSED_FIELDS

    [SerializeField] private Room[] _rooms;
    [SerializeField] private Room _currentRoom;
    [SerializeField] private Transform _player;
    [SerializeField] private Image _arrowImage;
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private Player_Data_Source _playerData;
    private bool pause;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        _playerInputManager.OnPlayerPause += OnGamePause;
        EnemiesManager.OnNoEnemies += SetCanMoveForward;
        
    }
    
    private void OnInsuficientHealth()
    {
        _losePanel.SetActive(true);
        OnPause?.Invoke();
    }

    private void Start()
    {
        if(_playerData == null)
            _playerData = FindObjectOfType<Player_Data_Source>();

        _playerData._player.character_Health_Component.OnInsufficient_Health += OnInsuficientHealth;
        
        _arrowImage.enabled = false;
        _currentRoom = _rooms[0];
        _currentRoom._canMoveForward = false;
        pause = false;
        Debug.Log("Current Room: " + _currentRoom.roomNumber);
    }

    private void Update()
    {
        if (CanMoveForward())
        {
            _arrowImage.enabled = true;
            _currentRoom.wall.SetActive(false);
        }
        else
        {
            _currentRoom.wall.SetActive(true);
            _arrowImage.enabled = false;
        }

        if (_player.position.x > _currentRoom.wall.transform.position.x)
        {
            _currentRoom.wall.SetActive(true);
            _currentRoom = _rooms[_currentRoom.roomNumber];
            _currentRoom._canMoveForward = false;
            Debug.Log("Current Room: " + _currentRoom.roomNumber);
            OnNewRoom?.Invoke();
        }
    }

    private void OnGamePause()
    {
        if (!_playerData._player.isDead)
        {
            if (pause)
                UnPauseGame();
            else
                PauseGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerChecks(other);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerChecks(other);
    }

    private void OnDestroy()
    {
        EnemiesManager.OnNoEnemies -= SetCanMoveForward;
        _playerData._player.character_Health_Component.OnInsufficient_Health += OnInsuficientHealth;
    }

    #endregion

    #region PUBLIC_METHODS

    public bool CanMoveForward()
    {
        return _currentRoom._canMoveForward;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        pause = true;
        OnPause?.Invoke();
    }

    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        pause = false;
        OnUnPause?.Invoke();
    }

    public int GetCurrentRoom()
    {
        return _currentRoom.roomNumber;
    }
    

    #endregion

    #region PRIVATE_METHODS

    private void SetCanMoveForward()
    {
        _currentRoom._canMoveForward = true;
    }

    private void PlayerChecks(Collider other)
    {
        if (other.CompareTag(_playerTag) && _currentRoom._canMoveForward)
        {
            _arrowImage.enabled = true;
        }
        else if (other.CompareTag(_playerTag) && !_currentRoom._canMoveForward)
        {
            _arrowImage.enabled = false;
        }
    }

    #endregion
}