using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
   [SerializeField] private GameObject loadingScreen;
   [SerializeField] private Slider loadingBar;
   public void LoadScene(int levelIndex)
   {
      StartCoroutine(LoadSceneAsynchronusly(levelIndex));
   }

   private IEnumerator LoadSceneAsynchronusly(int levelIndex)
   {
      AsyncOperation operation =  SceneManager.LoadSceneAsync(levelIndex);
      loadingScreen.SetActive(true);
      while (!operation.isDone)
      {
         loadingBar.value = operation.progress;
         yield return null;
      }
   }
}
