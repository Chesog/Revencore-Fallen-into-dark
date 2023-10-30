using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IInteractable
{
    [SerializeField] private float healValue;

    public void PickUp(CharacterComponent user)
    {
        throw new System.NotImplementedException();
    }

    public void Drop(CharacterComponent user)
    {
        throw new System.NotImplementedException();
    }

    public void Interact(CharacterComponent user)
    {
        user.character_Health_Component.IncreaseHealth(healValue);
        Destroy(this.gameObject);
    }
}