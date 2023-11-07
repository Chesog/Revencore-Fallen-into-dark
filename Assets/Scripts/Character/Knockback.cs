using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Knockback : MonoBehaviour
{
    #region EVENTS

    public event Action OnBeginKnockback, OnDoneKnockback;

    #endregion

    #region EXPOSED_FIELDS

    [SerializeField] private float _strength = 16;
    [SerializeField] private float _delay = 0.15f;

    #endregion

    #region PUBLIC_METHODS

    public void PlayKnockback(Transform sender)
    {
        StopAllCoroutines();
        OnBeginKnockback?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        direction.y = 0;
        StartCoroutine(KnockbackCoroutine(direction));
    }

    #endregion

    #region PRIVATE_METHODS

    private IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        float timer = 0;
        float originalZ = transform.position.z; 

        while (timer < _delay)
        {
            Vector3 newPosition = (Vector2)transform.position + direction * (_strength * Time.deltaTime);
            newPosition.z = originalZ; 
            transform.position = newPosition;

            timer += Time.deltaTime;
            yield return null;
        }

        OnDoneKnockback?.Invoke();
    }

    #endregion
}