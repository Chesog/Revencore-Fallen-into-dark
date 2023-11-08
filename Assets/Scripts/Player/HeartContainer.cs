using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    [SerializeField] private HeartStates currentState;
    [SerializeField] private string healthAnimatorParameter = "Health";
    [SerializeField] private Animator _animator;
    [SerializeField] private Image heartContainer;
    [SerializeField] private Sprite[] heartStates;

    public void SetHeartState(HeartStates newStates)
    {
        currentState = newStates;
    }
    
    public void SetHeartState(int value)
    {
        Debug.Log(gameObject.name + " Heart Value " + value);
        _animator.SetInteger(healthAnimatorParameter,value);
    }

    public void UpdateHeart()
    {
        switch (currentState)
        {
            case HeartStates.FULLHEART:
                heartContainer.sprite = heartStates[(int)HeartStates.FULLHEART];
                _animator.SetInteger("Health",2);
                break;
            case HeartStates.HALFHEART:
                heartContainer.sprite = heartStates[(int)HeartStates.HALFHEART];
                _animator.SetInteger("Health",1);
                break;
            case HeartStates.EMPTYHEART:
                heartContainer.sprite = heartStates[(int)HeartStates.EMPTYHEART];
                _animator.SetInteger("Health",0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
