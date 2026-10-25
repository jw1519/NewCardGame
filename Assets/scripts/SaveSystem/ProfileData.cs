using System.Collections.Generic;


[System.Serializable]
public class ProfileData
{
    public string profileName;
    public List<string> enemiesFound;
    public GameData savedGameData;
    public ProfileData()
    {
        profileName = "profile";
        enemiesFound = new();
        savedGameData = new GameData();
    }
}
