using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_T : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
            Destroy(gameObject);
    }
}
