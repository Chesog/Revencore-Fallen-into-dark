using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private float _gravityScale = 1f;
    [SerializeField] private float _explosionRadius = 2f;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private GameObject _effectPrefab;

    #endregion

    #region PRIVATE_FIELDS

    private bool _hasExploded = false;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * _gravityScale);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_hasExploded)
        {
            Explode();
            _hasExploded = true;
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private void Explode()
    {
        PlayEffect();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player") &&
                !collider.GetComponentInParent<PlayerComponent>().isPlayer_Damaged)
            {
                collider.GetComponentInParent<HealthComponent>().DecreaseHealth(_damage);

                Knockback knockback = collider.GetComponentInParent<Knockback>();
                if (knockback != null)
                    knockback.PlayKnockback(transform);
            }
        }

        Destroy(gameObject);
    }

    private void PlayEffect()
    {
        GameObject effectInstance = Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        Destroy(effectInstance, 1.0f);
    }

    #endregion
}