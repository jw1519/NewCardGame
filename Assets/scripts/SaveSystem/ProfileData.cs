using System.Collections.Generic;


[System.Serializable]
public class ProfileData
{
    public string profileName;
    public List<string> enemiesFound;
    public ProfileData()
    {
        profileName = "profile";
        enemiesFound = new();
    }
}
