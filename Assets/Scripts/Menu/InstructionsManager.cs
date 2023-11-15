using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject[] instructions;
    [SerializeField] private GameObject[] credits;

    private void Start()
    {
        instructionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void ChangePage(int value,GameObject[] target)
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
        ChangePage(pageIndex,instructions);
    }

    public void GotoPage(int pageIndex)
    {
        ChangePage(pageIndex,credits);
    }

    public void ShowInstructionsMenu()
    {
        instructionsMenu.SetActive(true);
    }

    public void ShowCreditsMenu()
    {
        creditsMenu.SetActive(true);
    }

    public void HideCredits()
    {
        creditsMenu.SetActive(false);
    }
}
