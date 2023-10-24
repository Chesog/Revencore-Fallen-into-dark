using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public event Action OnBeginKnockback, OnDoneKnockback;
    
    [SerializeField] private float strength = 16;
    [SerializeField] private float delay = 0.15f;

    public void PlayKnockback(Transform sender)
    {
        StopAllCoroutines();
        OnBeginKnockback?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        direction.y = 0; 
        StartCoroutine(KnockbackCoroutine(direction));
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        float timer = 0;
        float originalZ = transform.position.z; // Save the original z value

        while (timer < delay)
        {
            Vector3 newPosition = (Vector2)transform.position + direction * (strength * Time.deltaTime);
            newPosition.z = originalZ; // Restore the original z value
            transform.position = newPosition;

            timer += Time.deltaTime;
            yield return null;
        }

        OnDoneKnockback?.Invoke();
    }

    // private IEnumerator Reset()
    // {
    //     yield return new WaitForSeconds(delay);
    //     _rb.velocity = Vector3.zero;
    //     OnDoneKnockback?.Invoke();
    // }
}
