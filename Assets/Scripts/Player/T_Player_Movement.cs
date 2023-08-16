using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Player_Movement : MonoBehaviour
{
    [SerializeField] Vector3 movement = Vector3.zero;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float speed;
    [SerializeField] float z_speed;
    [SerializeField] float jumpForce;
    void Update()
    {
        z_speed = speed / 2;

        movement.x = Input.GetAxisRaw("Horizontal") * z_speed;
        movement.z = Input.GetAxisRaw("Vertical") * speed;

        if (Input.GetKey(KeyCode.D)) 
        {
            transform.forward = Vector3.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.forward = Vector3.left;
        }

        movement.y = rigidbody.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        rigidbody.velocity = movement;
    }
}
