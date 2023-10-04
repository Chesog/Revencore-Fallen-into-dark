using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
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
    [SerializeField] private Image _image;
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerInputManager _playerInputManager;
    private bool pause;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        _playerInputManager.OnPlayerPause += OnGamePause;
        EnemiesManager_T.NoEnemies += SetCanMoveForward;
    }

    private void Start()
    {
        _image.enabled = false;
        _currentRoom = _rooms[0];
        _currentRoom._canMoveForward = false;
        Debug.Log("Current Room: " + _currentRoom.roomNumber);
    }

    private void Update()
    {
        if (CanMoveForward())
        {
            _image.enabled = true;
            _currentRoom.wall.SetActive(false);
        }
        else
        {
            _currentRoom.wall.SetActive(true);
            _image.enabled = false;
        }

        if (_player.position.x > _currentRoom.wall.transform.position.x)
        {
            _currentRoom.wall.SetActive(true);
            _currentRoom = _rooms[_currentRoom.roomNumber];
            _currentRoom._canMoveForward = false;
            Debug.Log("Current Room: " + _currentRoom.roomNumber);
        }
    }

    private void OnGamePause()
    {
        if (pause)
            UnPauseGame();
        else
            PauseGame();
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
        EnemiesManager_T.NoEnemies -= SetCanMoveForward;
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
        pause = true;
        Time.timeScale = 0.1f;
    }

    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        pause = false;
        Time.timeScale = 1;
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
            _image.enabled = true;
        }
        else if (other.CompareTag(_playerTag) && !_currentRoom._canMoveForward)
        {
            _image.enabled = false;
        }
    }

    #endregion
}