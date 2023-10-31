using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWwiseEvent : MonoBehaviour
{
    [SerializeField] string akEvent; 
    
    public void OnClick() { 
        AkSoundEngine.PostEvent(akEvent, gameObject); 
    }
}
