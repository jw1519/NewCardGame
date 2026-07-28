using UnityEngine.SceneManagement;

public class PausePanel : BasePanel
{
    public void EndRun()
    {
        ClosePanel();
        AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("GameOverPanel").OpenPanel();
    }
    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
