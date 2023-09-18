using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
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
    #endregion

    #region PRIVATE_FIELDS
    private bool _canMoveForward;
    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        EnemiesManager_T.NoEnemies += SetCanMoveForward;
    }

    private void Start()
    {
        _image.enabled = false;

        foreach (Room room in _rooms)
        {
            Debug.Log("Room Number: " + room.roomNumber + ", Wall: " + room.wall.name);
        }

        _currentRoom = _rooms[0];
        _currentRoom._canMoveForward = false;
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
            _currentRoom = _rooms[1];
            _currentRoom._canMoveForward = false;
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
        EnemiesManager_T.NoEnemies -= SetCanMoveForward;
    }
    #endregion

    #region PUBLIC_METHODS
    public bool CanMoveForward()
    {
        return _currentRoom._canMoveForward;
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