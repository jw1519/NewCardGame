using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    UIManager UIManager;
    private void Awake()
    {
        UIManager = GetComponent<UIManager>();
    }
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
    public void HowToPlay()
    {
        UIManager.GetPanel("HowToPlayPanel").OpenPanel();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
