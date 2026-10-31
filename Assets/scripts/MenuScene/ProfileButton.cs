using UnityEngine;

public class ProfileButton : MonoBehaviour
{
    public string profileName;
    public BasePanel panel;

    public void LoadProfile()
    {
        SaveManager.Instance.LoadProfile(profileName);
        panel.ClosePanel();
    }
}
