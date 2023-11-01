using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FloatingText : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private float _destroyTime = 3f;

    #endregion

    #region UNITY_CALLS

    void Start()
    {
        Destroy(gameObject, _destroyTime);
    }

    #endregion
}