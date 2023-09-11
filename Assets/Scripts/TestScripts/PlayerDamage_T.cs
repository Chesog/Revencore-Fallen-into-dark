using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage_T : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private string enemyBulletTag = "EnemyBullet";
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Transform floatingTextSpawn;
    public float damage = 20;


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
        Debug.Log("Player HP: " + currentHealth.ToString());

        if (floatingTextPrefab)
            ShowFloatingText();

        CheckHealth();
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("El player murio!");
            SceneManager.LoadScene("Test_Scene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(enemyBulletTag))
        {
            TakeDamage(30f);

        }
    }
    private void ShowFloatingText()
    {
        var go = Instantiate(floatingTextPrefab, floatingTextSpawn.position, Quaternion.identity, transform);
        go.SetActive(true);
        go.GetComponent<TextMesh>().text = currentHealth.ToString();
    }

}
