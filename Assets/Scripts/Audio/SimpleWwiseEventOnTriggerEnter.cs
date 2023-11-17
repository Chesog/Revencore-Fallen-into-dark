using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWwiseEventOnTriggerEnter : MonoBehaviour
{
    [SerializeField] string akEvent; 
    
    void OnTriggerEnter() { 
        AkSoundEngine.PostEvent(akEvent, gameObject); 
    }
}
