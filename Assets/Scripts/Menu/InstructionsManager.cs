using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionsMenu;
    [SerializeField] private GameObject[] instructions;

    private void Start()
    {
        instructionsMenu.SetActive(false);
    }

    public void ChangePage(int value)
    {
        for (int i = 0; i < instructions.Length; i++)
        {
            if (i == value)
                instructions[i].SetActive(true);
            else
                instructions[i].SetActive(false);
        }
    }

    public void GoToNextPage(int pageIndex)
    {
        ChangePage(pageIndex);
    }

    public void ShowInstructionsMenu()
    {
        instructionsMenu.SetActive(true);
    }
}
