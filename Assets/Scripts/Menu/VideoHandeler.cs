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
        imageAlpha = new Color(1,1,1,1);
        _videoPlayerImage.color = imageAlpha;
        _videoPlayer.Play();
    }

    public void StopVideo()
    {
        imageAlpha = new Color(1,1,1,0);
        _videoPlayerImage.color = imageAlpha;
        _videoPlayer.Stop();
        _videoPlayer.time = 0;
        _videoPlayer.frame = 0;
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

    public bool IsPaused()
    {
        return _videoPlayer.isPaused;
    }
}
