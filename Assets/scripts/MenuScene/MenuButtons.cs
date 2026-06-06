using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void NewRun()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void LoadRun()
    {
        
    }
    public void Settings()
    {
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
