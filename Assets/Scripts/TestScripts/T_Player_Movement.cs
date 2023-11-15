using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Player_Movement : MonoBehaviour
{
    [SerializeField] private Vector3 movement = Vector3.zero;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private float speed;
    [SerializeField] private float z_speed;
    [SerializeField] private float jumpForce;
    private bool pause;
    void Update()
    {
        z_speed = speed / 2;
        if (!pause) 
        {
            movement.x = Input.GetAxisRaw("Horizontal") * speed;
            movement.z = Input.GetAxisRaw("Vertical") * z_speed;

            if (Input.GetKey(KeyCode.D))
            {
                transform.forward = Vector3.forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.forward = Vector3.back;
            }

            movement.y = rigidbody.velocity.y;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeInHierarchy)
                UnPauseGame();
            else
                PauseGame();
        }
        rigidbody.velocity = movement;
    }

    public void PauseGame() 
    {
        pauseMenu.SetActive(true);
        pause = true;
    }
    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        pause = false;
    }
}
