using UnityEngine;

public class ProfileButton : MonoBehaviour
{
    public string profileName;

    public void LoadProfile()
    {
        SaveManager.Instance.LoadProfile(profileName);
    }
    public void NewProfile()
    {
        SaveManager.Instance.NewProfile(profileName);
    }
}
