using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeart : MonoBehaviour
{
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private CameraShaker _cameraShake;
    [SerializeField] private PlayerComponent _player;
    [SerializeField] private Transform _outsideTeleport;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        if (_healthComponent == null)
        {
            _healthComponent = GetComponent<HealthComponent>();
        }

        if (!_healthComponent)
        {
            Debug.LogError(message: $"{name}: (logError){nameof(_healthComponent)} is null");
            enabled = false;
        }
        _healthComponent.OnInsufficient_Health += HealthComponentOnOnInsufficient_Health;
    }

    private void HealthComponentOnOnInsufficient_Health()
    {
        _cameraShake.StartCameraShake();
        AkSoundEngine.PostEvent("BossScream", gameObject);
        // Poner Video Animacion
        _animator.Play("Fade");
        StartCoroutine(TeleportAfterDelay(1.30f));
    }
    
    private IEnumerator TeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _player.TeleportPlayer(_outsideTeleport.position);
    }
}
