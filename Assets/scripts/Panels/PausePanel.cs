using TMPro;
using UnityEngine.SceneManagement;

public class PausePanel : BasePanel
{
    public TextMeshProUGUI seedText;
    public override void ClosePanel()
    {
        base.ClosePanel();
        AssetManager.Instance.GetAsset("AudioManager").GetComponent<AudioManager>().Play("UIButtonPress");
    }
    public void EndRun()
    {
        AssetManager.Instance.GetAsset("AudioManager").GetComponent<AudioManager>().Play("UIButtonPress");
        ClosePanel();
        AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("GameOverPanel").OpenPanel();
    }
    public void Save()
    {
        AssetManager.Instance.GetAsset("AudioManager").GetComponent<AudioManager>().Play("UIButtonPress");
        SaveManager.Instance.SaveGame();
    }
    public void Quit()
    {
        AssetManager.Instance.GetAsset("AudioManager").GetComponent<AudioManager>().Play("UIButtonPress");
        SceneManager.LoadScene("Menu");
    }
}
