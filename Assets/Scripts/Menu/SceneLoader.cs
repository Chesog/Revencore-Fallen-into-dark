using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
   [SerializeField] private GameObject loadingScreen;
   [SerializeField] private GameObject skipButton;
   [SerializeField] private GameObject loadingPanel;
   [SerializeField] private Slider loadingBar;
   [SerializeField] private VideoHandeler _videoHandeler;

   private void OnEnable()
   {
      skipButton.SetActive(false);
      loadingPanel.SetActive(false);
   }

   public void LoadScene(int levelIndex)
   {
      skipButton.SetActive(true);
      StartCoroutine(PlayVideoAndLoadScene(levelIndex));
   }

   private IEnumerator PlayVideoAndLoadScene(int levelIndex)
   {
      _videoHandeler.PlayVideo();

      
      while (!_videoHandeler.IsPaused())
      {
         yield return null;
      }
      
      StartCoroutine(LoadSceneAsynchronusly(levelIndex));
   }

   public void StopIntroVideo(int levelIndex)
   {
      StartCoroutine(LoadSceneAsynchronusly(levelIndex));
      loadingPanel.SetActive(true);
      _videoHandeler.StopVideo();
   }

   private IEnumerator LoadSceneAsynchronusly(int levelIndex)
   {
      AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
      loadingScreen.SetActive(true);
      while (!operation.isDone)
      {
         loadingBar.value = operation.progress;
         yield return null;
      }
   }
}
