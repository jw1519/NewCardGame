using UnityEngine;

public class ProfileButton : MonoBehaviour
{
    public string profileName;
    public BasePanel panel;

    private void Start()
    {
        panel = GetComponentInParent<BasePanel>();
    }

    public void LoadProfile()
    {
        SaveManager.Instance.LoadProfile(profileName);
        panel.ClosePanel();
    }
}
