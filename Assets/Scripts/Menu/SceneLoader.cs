using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
   [SerializeField] private GameObject loadingScreen;
   [SerializeField] private Slider loadingBar;
   [SerializeField] private VideoHandeler _videoHandeler;
   public void LoadScene(int levelIndex)
   {
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
