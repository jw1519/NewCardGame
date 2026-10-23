using TMPro;
using UnityEngine.SceneManagement;

public class PausePanel : BasePanel
{
    public TextMeshProUGUI seedText;
    public void EndRun()
    {
        AssetManager.Instance.GetAsset("AudioManager").GetComponent<AudioManager>().Play("UIButtonPress");
        ClosePanel();
        AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("GameOverPanel").OpenPanel();
    }
    public void Quit()
    {
        AssetManager.Instance.GetAsset("AudioManager").GetComponent<AudioManager>().Play("UIButtonPress");
        SceneManager.LoadScene("Menu");
    }
}
