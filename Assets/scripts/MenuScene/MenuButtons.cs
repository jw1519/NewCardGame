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
        SaveManager.loadOnStart = false;
        SceneManager.LoadScene("GameScene");
    }
    public void LoadRun()
    {
        SaveManager.loadOnStart = true;
        Debug.Log(SaveManager.loadOnStart);
        SceneManager.LoadScene("GameScene");
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
