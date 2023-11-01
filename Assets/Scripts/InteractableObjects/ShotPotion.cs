using UnityEngine;

public class ShotPotion : MonoBehaviour,IInteractable
{
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
        user.isRanged_Attacking = true;
        Destroy(this.gameObject);
    }
}
