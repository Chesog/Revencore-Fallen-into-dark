using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu_T : MonoBehaviour
{
    [SerializeField] private string level1SceneName;
    //[SerializeField] private string creditsSceneName;
    [SerializeField] private string mainMenuSceneName;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PlayLevel1()
    {
        LoadScene(level1SceneName);
    }

    //public void ShowCredits()
    //{
    //    LoadScene(creditsSceneName);
    //}
    
    public void GoMainMenu()
    {
        LoadScene(mainMenuSceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
