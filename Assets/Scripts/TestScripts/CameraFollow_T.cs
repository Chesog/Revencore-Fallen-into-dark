using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_T : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime;
    #endregion

    #region PRIVATE_FIELDS
    private Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;
    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        _offset = transform.position - _target.position;
    }

    private void Update()
    {
        Vector3 targetPosition = _target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
    }
    #endregion
}
