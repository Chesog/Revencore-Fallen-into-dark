using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_T : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;
    Quaternion rot;

    private void Update()
    {
        if (transform.rotation.y == 0f)
            rot = Quaternion.EulerRotation(0, transform.rotation.y + 89.5f, transform.rotation.z);
        else
            rot = Quaternion.EulerRotation(0, transform.rotation.y - 90.5f, transform.rotation.z);

        if (Input.GetKeyDown(KeyCode.E))
            Instantiate(bulletPrefab, shootingPoint.position, rot);
    }

}
