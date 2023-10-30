using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeartUI : MonoBehaviour
{
    [SerializeField] Player_Data_Source _Source;
    [SerializeField] Image[] heartContainer;
    [SerializeField] Sprite[] heartStates;

    // Start is called before the first frame update
    void Start()
    {
        _Source._player.character_Health_Component.OnDecrease_Health += OnPlayerUpdateHealth;
        _Source._player.character_Health_Component.OnIncrease_Health += OnPlayerUpdateHealth;
        if (_Source._player.character_Health_Component._health == _Source._player.character_Health_Component._maxHealth) 
        {
            for (int i = 0; i < heartContainer.Length; i++)
            {
                heartContainer[i].sprite = heartStates[0];
            }
        }

    }

    private void OnPlayerUpdateHealth()
    {
        int fullHearts = (int)(_Source._player.character_Health_Component._health / 2);
        bool hasHalfHeart = _Source._player.character_Health_Component._health % 2 == 1;

        for (int i = 0; i < heartContainer.Length; i++)
        {
            if (i < fullHearts)
            {
                // Draw full heart
                heartContainer[i].sprite = heartStates[0];
            }
            else if (i == fullHearts && hasHalfHeart)
            {
                // Draw half heart
                heartContainer[i].sprite = heartStates[1];
            }
            else
            {
                // Draw empty heart
                heartContainer[i].sprite = heartStates[2];
            }
        }
    }

    private void OnDestroy()
    {
        _Source._player.character_Health_Component.OnDecrease_Health -= OnPlayerUpdateHealth;
        if (_Source._player.character_Health_Component._health <= 0)
        {
            for (int i = 0; i < heartContainer.Length; i++)
            {
                heartContainer[i].sprite = heartStates[2];
            }
        }
    }
}
