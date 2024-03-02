using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    #region EVENTS
    public static event Action OnDialogueFinished;

    #endregion
    
    [SerializeField] private PlayerComponent _playerComponent;
    [SerializeField] private GameObject _dialogueTemplate;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _arrowSprite;
    [SerializeField] private PlayerComponent _player;
    [SerializeField] private Transform _houseTeleport;
    [SerializeField] private TextMeshProUGUI _textObject;
    [SerializeField] private Animator _animator;

    //[SerializeField] private float _delayBetweenDialogues = 5f;
    [SerializeField] private string[] _texts;
    [SerializeField] private string _playerTag = "Player";
    private bool _playerDetection = false;
    private bool _canDialogue = false;
    private bool _alreadyDialogue = false;
    private int _currentDialogueIndex = 0;

    private void Awake()
    {
        RoomManager.OnDialogue += OnDialogue;
    }

    private void Update()
    {
        if (!_alreadyDialogue)
        {
            if (_playerDetection && !_playerComponent.isDialogue && _canDialogue)
            {
                _alreadyDialogue = true;
                _canvas.SetActive(true);
                _playerComponent.isDialogue = true;
                ShowNextDialogue();
                _dialogueTemplate.SetActive(true);
            }
        }
    }

    private void OnDialogue()
    {
        if (!_alreadyDialogue)
        {
            _canDialogue = true;
            _arrowSprite.SetActive(true);
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
            _arrowSprite.SetActive(false);
            _animator.Play("Fade");
            OnDialogueFinished?.Invoke();
            StartCoroutine(TeleportAfterDelay(1.30f));
        }
    }
    
    private IEnumerator TeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _player.TeleportPlayer(_houseTeleport.position);
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

    private void OnDestroy()
    {
        RoomManager.OnDialogue -= OnDialogue;
    }

    public bool GetAlreadyDialogue()
    {
        return _alreadyDialogue;
    }
}