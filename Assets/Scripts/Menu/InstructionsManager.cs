using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private GameObject[] instructions;
    [SerializeField] private GameObject[] credits;
    [SerializeField] private bool creditsScene;

    private void Start()
    {
        menuButtons.SetActive(true);

        if (instructionsMenu != null)
            instructionsMenu.SetActive(false);
        if (creditsMenu != null)
            creditsMenu.SetActive(false);
        
        if (creditsScene)
            creditsMenu.SetActive(true);
    }

    public void ChangePage(int value, GameObject[] target)
    {
        for (int i = 0; i < target.Length; i++)
        {
            if (i == value)
                target[i].SetActive(true);
            else
                target[i].SetActive(false);
        }
    }

    public void GoToNextPage(int pageIndex)
    {
        ChangePage(pageIndex, instructions);
    }

    public void GotoPage(int pageIndex)
    {
        ChangePage(pageIndex, credits);
    }

    public void ShowInstructionsMenu()
    {
        instructionsMenu.SetActive(true);
    }
    public void HideInstructionsMenu()
    {
        instructionsMenu.SetActive(false);
    }

    public void ToggleOptions()
    {
        if (OptionsMenu.activeInHierarchy)
        {
            instructionsMenu.SetActive(true);
            OptionsMenu.SetActive(false);
        }
        else
        {
            instructionsMenu.SetActive(false);
            OptionsMenu.SetActive(true);
        }
    }

    public void ShowCreditsMenu()
    {
        menuButtons.SetActive(false);
        creditsMenu.SetActive(true);
        ChangePage(0, credits);
    }

    public void HideCredits()
    {
        creditsMenu.SetActive(false);
    }
}