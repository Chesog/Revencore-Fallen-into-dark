using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText_T : MonoBehaviour
{
    [SerializeField] private float destroyTime = 3f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

}
