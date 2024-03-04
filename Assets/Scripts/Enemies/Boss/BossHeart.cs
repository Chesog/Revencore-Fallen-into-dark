using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeart : MonoBehaviour
{
    #region EVENTS
    public static event Action OnDialogueFinished;
    

    #endregion
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private CameraShaker _cameraShake;
    [SerializeField] private PlayerComponent _player;
    [SerializeField] private Transform _outsideTeleport;
    [SerializeField] private Animator _animator;
    [SerializeField] private VideoHandeler _videoHandeler;
    
    private bool _canDestroyHeart = false;

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
        EnemiesManager.OnNoHouseEnemies += OnCanDestroyHeart;
    }

    private void OnCanDestroyHeart()
    {
        _canDestroyHeart = true;
    }

    private void HealthComponentOnOnInsufficient_Health()
    {
        if (_canDestroyHeart)
        {
            _cameraShake.StartCameraShake();
            AkSoundEngine.PostEvent("BossScream", gameObject);
            // Poner Video Animacion
            _animator.Play("Fade");
            StartCoroutine(TeleportAfterDelay(1.30f));
        }
    }
    
    private IEnumerator TeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _player.TeleportPlayer(_outsideTeleport.position);
        OnDialogueFinished?.Invoke();
    }
    
}
