using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoHandeler : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    private void OnEnable()
    {
        StopVideo();
    }

    public void PlayVideo()
    {
        _videoPlayer.Play();
    }

    public void StopVideo()
    {
        //gameObject.SetActive(false);
        _videoPlayer.Stop();
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
        StopVideo();
    }

    private void OnDestroy()
    {
        gameObject.SetActive(false);
        StopVideo();
    }
}
