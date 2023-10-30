using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void PickUp(CharacterComponent user);
    void Drop(CharacterComponent user);
    void Interact(CharacterComponent user);
}
