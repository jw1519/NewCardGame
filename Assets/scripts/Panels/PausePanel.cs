using UnityEngine.SceneManagement;

public class PausePanel : BasePanel
{
    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
