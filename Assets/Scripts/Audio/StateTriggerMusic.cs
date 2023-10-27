using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTriggerMusic : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.State musicState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            musicState.SetValue();
    }

}
