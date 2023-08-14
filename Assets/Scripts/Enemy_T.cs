using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_T : MonoBehaviour
{
    [SerializeField] private string bulletTag = "Bullet";
    [SerializeField] private GameObject floatingTextPrefab;
    private float currentHealth;

    void Start()
    {
        currentHealth = 1000;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(bulletTag))
        {
            Debug.Log("Lo mataste, ASESINO!");
            currentHealth -= 20;

            if (floatingTextPrefab)
            {
                ShowFloatingText();
            }

        }
    }

    private void ShowFloatingText()
    {
       var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = currentHealth.ToString();
    }
}
