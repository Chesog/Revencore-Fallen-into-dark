using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_T : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(bulletPrefab, shootingPoint.position,transform.rotation);
        }
    }

}
