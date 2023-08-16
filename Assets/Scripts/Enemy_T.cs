using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_T : MonoBehaviour
{
    [SerializeField] private string bulletTag = "Bullet";
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3f;
    private float currentHealth;
    private float distance;

    void Start()
    {
        currentHealth = 1000;
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        Vector3 direction = player.position - transform.position;

        if (distance > 1.2f) 
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if(!IsAlive())
            Destroy(gameObject);
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

    private bool IsAlive()
    {
        return currentHealth >= 0;
    }
}
