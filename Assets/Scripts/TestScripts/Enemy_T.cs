using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Enemy_T : MonoBehaviour
{
    #region EVENTS
    public static event Action Destroyed;
    #endregion

    #region EXPOSED_FIELDS
    [SerializeField] private string bulletTag = "Bullet";
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Transform floatingTextSpawn;
    [SerializeField] private PlayerDamage_T player;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private float damageCooldown = 2f;
    [SerializeField] private float maxHealth = 100f;
    #endregion

    #region PRIVATE_FIELDS
    private float currentHealth;
    private float distance;
    private bool canTakeDamage = true;
    private float damageTimer = 0f;
    #endregion

    #region UNITY_CALLS
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 direction = player.transform.position - transform.position;

        if (distance > 1.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (canTakeDamage)
            {
                player.TakeDamage(damage);
                canTakeDamage = false;
                damageTimer = damageCooldown;
            }
        }

        if (!IsAlive())
        {
            Destroy(gameObject);
        }

        if (!canTakeDamage)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0f)
            {
                canTakeDamage = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(bulletTag))
        { 
            TakeDamage(player.damage);
        }
    }
    private void OnDestroy()
    {
        Debug.Log("Mataste un enemigo melee!");
        Destroyed?.Invoke();
    }
    #endregion

    #region PUBLIC_METHODS
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (floatingTextPrefab)
            ShowFloatingText();
    }
    #endregion

    #region PRIVATE_METHODS
    private void ShowFloatingText()
    {
       var go = Instantiate(floatingTextPrefab, floatingTextSpawn.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = currentHealth.ToString();
    }

    private bool IsAlive()
    {
        return currentHealth > 0;
    }
    #endregion
}
