using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instance;

    //Singleton para que no se destruya el objeto Audio Manager
    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
        }

        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
