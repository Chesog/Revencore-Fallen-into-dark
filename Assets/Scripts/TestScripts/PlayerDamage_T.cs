using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage_T : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        CheckHealth();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth.ToString());
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Test_Scene");
        }
    }
}
