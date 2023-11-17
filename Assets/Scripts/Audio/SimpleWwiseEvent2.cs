using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWwiseEvent2 : MonoBehaviour
{
    [SerializeField] string[] akEvent; 
    
    public void Event0() {
        AkSoundEngine.PostEvent(akEvent[0], gameObject); 
    }
    public void Event1()
    {
        AkSoundEngine.PostEvent(akEvent[1], gameObject);
    }
    public void Event2()
    {
        AkSoundEngine.PostEvent(akEvent[2], gameObject);
    }
    public void Event3()
    {
        AkSoundEngine.PostEvent(akEvent[3], gameObject);
    }
    public void Event4()
    {
        AkSoundEngine.PostEvent(akEvent[4], gameObject);
    }
}
