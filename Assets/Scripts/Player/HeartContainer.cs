using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Image heartContainer;
    [SerializeField] private Sprite[] heartStates;
    private HeartStates currentState;

    public void SetHeartState(HeartStates newStates)
    {
        currentState = newStates;
    }

    public void UpdateHeart()
    {
        switch (currentState)
        {
            case HeartStates.FULLHEART:
                break;
            case HeartStates.HALFHEART:
                break;
            case HeartStates.EMPTYHEART:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
