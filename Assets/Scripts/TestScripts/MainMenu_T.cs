using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu_T : MonoBehaviour
{
    [SerializeField] private string level1SceneName = "Test_Scene";
    [SerializeField] private string creditsSceneName = "Credits_Scene";
    [SerializeField] private string mainMenuSceneName = "Menu_Scene";

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PlayLevel1()
    {
        LoadScene(level1SceneName);
    }

    public void ShowCredits()
    {
        LoadScene(creditsSceneName);
    }

    public void GoMainMenu()
    {
        LoadScene(mainMenuSceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
