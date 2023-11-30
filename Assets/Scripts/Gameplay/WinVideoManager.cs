using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinVideoManager : MonoBehaviour
{
    [SerializeField] private VideoHandeler _videoHandeler;
    [SerializeField] private GameObject _winningPanel;

    private void Start()
    {
        _winningPanel.SetActive(false);
       StartCoroutine(ShowVideo());
    }

    private IEnumerator ShowVideo()
    {
        _videoHandeler.PlayVideo();

        while (!_videoHandeler.IsPaused())
        {
            yield return null;
        }
        
        _winningPanel.SetActive(true);

    }
}
