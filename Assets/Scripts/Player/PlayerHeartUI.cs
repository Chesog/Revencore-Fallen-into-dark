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
        _Source._player.character_Health_Component.OnDecrease_Health += OnPlayerDecreaseHealth;
        if (_Source._player.character_Health_Component._health == _Source._player.character_Health_Component._maxHealth) 
        {
            for (int i = 0; i < heartContainer.Length; i++)
            {
                heartContainer[i].sprite = heartStates[0];
            }
        }

    }

    private void OnPlayerDecreaseHealth()
    {
        for (int i = 0; i < heartContainer.Length; i++)
        {
            if (i < _Source._player.character_Health_Component._health / 2) 
            {
                heartContainer[i].sprite = heartStates[0];
            }
            else if (i < Mathf.Ceil(_Source._player.character_Health_Component._health / 2.0f))
            {
                heartContainer[i].sprite = heartStates[1];
            }
            else
            {
                heartContainer[i].sprite = heartStates[2];
            }
        }
    }

    private void OnDestroy()
    {
        _Source._player.character_Health_Component.OnDecrease_Health -= OnPlayerDecreaseHealth;
        if (_Source._player.character_Health_Component._health <= 0)
        {
            for (int i = 0; i < heartContainer.Length; i++)
            {
                heartContainer[i].sprite = heartStates[2];
            }
        }
    }
}
