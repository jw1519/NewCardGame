using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    UIManager UIManager;
    private void Start()
    {
        UIManager = GetComponent<UIManager>();
        if (SaveManager.Instance.selectedProfile == null)
            UIManager.GetPanel("ProfileSelectPanel").OpenPanel();
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
    public void ChangeProfile()
    {
        UIManager.GetPanel("ProfileSelectPanel").OpenPanel();
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
