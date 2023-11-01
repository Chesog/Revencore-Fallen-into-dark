using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu_T : MonoBehaviour
{
    [SerializeField] private string level1SceneName = "Test_Scene";
    [SerializeField] private string creditsSceneName = "Credits_Scene";
    [SerializeField] private string mainMenuSceneName = "Menu_Scene";
    [SerializeField] private string optionsSceneName = "Options_Scene";
    [SerializeField] private string leaderboardSceneName = "Leaderboard_Scene";

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
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
    
    public void GoOptions()
    {
        LoadScene(optionsSceneName);
    }
    
    public void GoLeaderboard()
    {
        LoadScene(leaderboardSceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
