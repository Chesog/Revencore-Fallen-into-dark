using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoHandeler : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private RawImage _videoPlayerImage;
    private Color imageAlpha;

    private void OnEnable()
    {
        StopVideo();
    }

    public void PlayVideo()
    {
        imageAlpha.a = 1.0f;
        _videoPlayerImage.color = imageAlpha;
        _videoPlayer.Play();
    }

    public void StopVideo()
    {
        imageAlpha.a = 0.0f;
        _videoPlayerImage.color = imageAlpha;
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
