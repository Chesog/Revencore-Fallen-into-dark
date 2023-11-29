using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_T : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private float _speed;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private string _enemyTag = "Enemy";
    [SerializeField] private string _parentTag;
    [SerializeField] private string _playerTag = "Player";

    #endregion

    #region PRIVATE_FIELDS

    private Rigidbody _rb;
    private Vector3 velocity;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (gameObject.CompareTag("Bullet"))
            velocity = new Vector3(0, -_speed, 0);
        else if (gameObject.CompareTag("EnemyBullet"))
        {
            velocity = new Vector3(_speed, 0, 0);
            if (transform.rotation.y < 0f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }


        Destroy(gameObject, 2.5f);
    }

    private void FixedUpdate()
    {
        transform.Translate(velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == _parentTag)
        {
            return;
        }

        if (collision.collider.tag == _enemyTag)
        {
            collision.collider.GetComponent<HealthComponent>().DecreaseHealth(_bulletDamage);
        }

        if (collision.collider.tag == _playerTag)
        {
            collision.collider.GetComponentInParent<HealthComponent>().DecreaseHealth(_bulletDamage);
        }

        Destroy(gameObject);
    }

    #endregion
}