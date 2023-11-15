using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private bool isCorrutineRuning;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float positionOffsetStrength;
    private Transform initialPos;

    private void Start()
    {
        isCorrutineRuning = false;
        initialPos = transform;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
            StartCameraShake();
    }

    public void StartCameraShake()
    {
        if (!isCorrutineRuning)
            StartCoroutine(CameraShakeCorutine());
    }

    private IEnumerator CameraShakeCorutine()
    {
        isCorrutineRuning = true;
        float elapsed = 0.0f;
        float curretnMagnitude = 1.0f;
        while (elapsed < shakeDuration)
        {
            float x = (Random.value - 0.5f) * curretnMagnitude * positionOffsetStrength;
            float y = ((Random.value + 0.5f) * initialPos.localPosition.y) * curretnMagnitude * positionOffsetStrength;

            transform.localPosition = new Vector3(x,initialPos.localPosition.y,initialPos.localPosition.z);

            elapsed += Time.deltaTime;
            curretnMagnitude = (1 - (elapsed / shakeDuration)) * (1 - (elapsed / shakeDuration));
            yield return null;
        }

        isCorrutineRuning = false;
        transform.localPosition = initialPos.localPosition;
    }
}