using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoHandeler : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    public void PlayVideo()
    {
        _videoPlayer.Play();
    }
}
