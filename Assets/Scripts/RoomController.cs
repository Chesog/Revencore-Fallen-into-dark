using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private int _roomNumber;
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
    }
    private void Update()
    {
        if (CanMoveForward())
        {
            _image.enabled = true;
        }
        else
        {
            _image.enabled = false;
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
        return _canMoveForward;
    }
    #endregion

    #region PRIVATE_METHODS
    private void SetCanMoveForward()
    {
        _canMoveForward = true;
    }

    private void PlayerChecks(Collider other)
    {
        if (other.CompareTag(_playerTag) && _canMoveForward)
        {
            _image.enabled = true;
        }
        else if (other.CompareTag(_playerTag) && !_canMoveForward)
        {
            _image.enabled = false;
        }
    }
    #endregion
}