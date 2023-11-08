using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum HeartStates
{
    FULLHEART,
    HALFHEART,
    EMPTYHEART
}
public class PlayerHeartUI : MonoBehaviour
{
    [SerializeField] Player_Data_Source _Source;
    [SerializeField] private HeartContainer[] heartContainers;

    // Start is called before the first frame update
    void Start()
    {
        _Source._player.character_Health_Component.OnDecrease_Health += OnPlayerUpdateHealth;
        _Source._player.character_Health_Component.OnIncrease_Health += OnPlayerUpdateHealth;
        if (_Source._player.character_Health_Component._health == _Source._player.character_Health_Component._maxHealth) 
        {
            for (int i = 0; i < heartContainers.Length; i++)
            {
                heartContainers[i].SetHeartState(HeartStates.FULLHEART);
                heartContainers[i].UpdateHeart();
                //heartContainers[i].SetHeartState(2);
            }
        }

    }

    private void OnPlayerUpdateHealth()
    {
        
        
        int fullHearts = (int)(_Source._player.character_Health_Component._health / 2);
        bool hasHalfHeart = _Source._player.character_Health_Component._health % 2 == 1;

        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < fullHearts)
            {
                // Draw full heart
                heartContainers[i].SetHeartState(HeartStates.FULLHEART);
                
            }
            else if (i == fullHearts && hasHalfHeart)
            {
                // Draw half heart
                heartContainers[i].SetHeartState(HeartStates.HALFHEART);
            }
            else
            {
                // Draw empty heart
                heartContainers[i].SetHeartState(HeartStates.EMPTYHEART);
            }
            
            heartContainers[i].UpdateHeart();
            //if (i < fullHearts)
            //{
            //    // Draw full heart
            //    heartContainers[i].SetHeartState(2);
            //}
            //else if (i == fullHearts && hasHalfHeart)
            //{
            //    // Draw half heart
            //    heartContainers[i].SetHeartState(1);
            //}
            //else
            //{
            //    // Draw empty heart
            //    heartContainers[i].SetHeartState(0);
            //}
        }
    }

    private void OnDestroy()
    {
        _Source._player.character_Health_Component.OnDecrease_Health -= OnPlayerUpdateHealth;
        if (_Source._player.character_Health_Component._health <= 0)
        {
            for (int i = 0; i < heartContainers.Length; i++)
            {
                heartContainers[i].SetHeartState(HeartStates.EMPTYHEART);
            }
        }
    }
}
