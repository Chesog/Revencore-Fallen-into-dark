using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class NPCSystem : MonoBehaviour
{
    [SerializeField] private PlayerComponent _playerComponent;
    [SerializeField] private GameObject _dialogueTemplate;
    [SerializeField] private GameObject _canvas;

    [SerializeField] private TextMeshProUGUI _textObject;

    //[SerializeField] private float _delayBetweenDialogues = 5f;
    [SerializeField] private string[] _texts;
    [SerializeField] private string _playerTag = "Player";
    private bool _playerDetection = false;
    private bool _alreadyDialogue = false;
    private int _currentDialogueIndex = 0;

    private void Update()
    {
        if (!_alreadyDialogue)
        {
            if (_playerDetection && !_playerComponent.isDialogue)
            {
                _alreadyDialogue = true;
                _canvas.SetActive(true);
                _playerComponent.isDialogue = true;
                ShowNextDialogue();
                _dialogueTemplate.SetActive(true);
            }
        }
    }

    private void ShowNextDialogue()
    {
        if (_currentDialogueIndex < _texts.Length)
        {
            StartCoroutine(ShowDialogueWithDelay(_texts[_currentDialogueIndex]));
            _currentDialogueIndex++;
        }
        else
        {
            _playerComponent.isDialogue = false;
            _dialogueTemplate.SetActive(false);
        }
    }

    private IEnumerator ShowDialogueWithDelay(string text)
    {
        NewDialogue(text);
        yield return new WaitForSeconds(5f);
        ShowNextDialogue();
    }

    private void NewDialogue(string text)
    {
        _textObject.text = text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
            _playerDetection = true;
        Debug.Log("Player Detected");
    }

    private void OnTriggerExit(Collider other)
    {
        _playerDetection = false;
    }
}