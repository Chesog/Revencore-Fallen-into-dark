using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_T : MonoBehaviour
{
    [SerializeField] private string bulletTag = "Bullet";
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(bulletTag))
        {
            Debug.Log("Lo mataste, ASESINO!");
        }
    }
}
