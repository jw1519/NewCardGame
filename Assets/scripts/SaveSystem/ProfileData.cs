using System.Collections.Generic;


[System.Serializable]
public class ProfileData
{
    public string profileName;
    public List<string> enemiesFound;
    public string savedGameDataName;
    public ProfileData()
    {
        profileName = "Profile ";
        enemiesFound = new();
        savedGameDataName = "";
    }
}
