using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static bool loadOnStart = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private string ProfileName;

    public static SaveManager Instance { get; private set; }

    public List<ISave> saveableObjects = new List<ISave>();
    FileDataHandler dataHandler;
    FileDataHandler profileHandler;
    GameData gameData;

    List<ProfileData> profileData;
    ProfileData selectedProfile;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }
    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        profileHandler = new FileDataHandler(Application.persistentDataPath, ProfileName);
        saveableObjects = FindAllSavableObjects();
        if (loadOnStart)
        {
            LoadGame();
        }
        else
        {
            NewGame();
        }
    }
    public void NewProfile(string name)
    {
        ProfileData data = new ProfileData();
        data.profileName = name;
        profileData.Add(data);
    }
    public void LoadProfile(string profileName)
    {
        foreach (ProfileData data in profileData)
        {
            if (data.profileName == profileName)
            {
                //selectedProfile = profileHandler.Load();
                
            }
        }
    }
    public List<ProfileData> GetProfileDatas()
    {
        return profileData;
    }

    public void NewGame()
    {
        gameData = new GameData();
    }
    public void LoadGame()
    {
        gameData = dataHandler.Load();
        if (gameData == null)
        {
            Debug.Log("GameData is null. creating new game");
            NewGame();
        }
        foreach (ISave savable in saveableObjects)
        {
            savable.LoadData(gameData);
        }
        UIManager uiManager = AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>();
        uiManager.CloseAllPanels();
        uiManager.GetPanel("PlayerStatsPanel").OpenPanel();
        uiManager.GetPanel("MapPanel").OpenPanel();
    }
    public void SaveGame()
    {
        foreach (ISave savable in saveableObjects)
        {
            savable.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }
    public List<ISave> FindAllSavableObjects()
    {
        IEnumerable<ISave> savableObjects = FindObjectsByType<MonoBehaviour>().OfType<ISave>();
        return new List<ISave>(savableObjects);
    }
    public void AddFoundEnemy(string enemyName)
    {
        if (!selectedProfile.enemiesFound.Contains(enemyName))
            selectedProfile.enemiesFound.Add(enemyName);
    }
}
