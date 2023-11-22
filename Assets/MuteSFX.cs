using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSFX : MonoBehaviour
{
    public string eventoMuteMusic = "SFXMute"; // Reemplaza con el nombre correcto de tu evento en Wwise
    public string eventoUnmuteMusic = "SFXUnMute"; // Reemplaza con el nombre correcto de tu evento en Wwise

    private bool musicMuteado = false;

    // Método llamado cuando se hace clic en el botón
    public void OnButtonClick()
    {
        // Alternar entre Mute y Unmute al hacer clic
        if (!musicMuteado)
        {
            // Disparar el evento de mute para el bus de música
            AkSoundEngine.PostEvent(eventoMuteMusic, gameObject);
        }
        else
        {
            // Disparar el evento de unmute para el bus de música
            AkSoundEngine.PostEvent(eventoUnmuteMusic, gameObject);
        }

        // Cambiar el estado de mute
        musicMuteado = !musicMuteado;
    }
}
