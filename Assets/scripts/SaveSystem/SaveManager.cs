using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    List<ProfileData> profileData = new();
    ProfileData selectedProfile;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;

        DontDestroyOnLoad(gameObject);
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    public void StartGame()
    {
        if (!SceneManager.GetSceneByName("GameScene").isLoaded) return;
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
        profileHandler = new FileDataHandler(Application.persistentDataPath, name);
        ProfileData data = new ProfileData();
        data.profileName = name;
        profileData.Add(data);
        selectedProfile = data;
    }
    public void LoadProfile(string profileName)
    {
        if (profileData.Find(p => p.profileName == profileName) == null)
        {
            NewProfile(profileName);
            return;
        }
        profileHandler = new FileDataHandler(Application.persistentDataPath, profileName);
        selectedProfile = profileHandler.LoadProfile();
        foreach (ProfileData data in profileData)
        {
            if (data.profileName == profileName)
            {
                selectedProfile = profileHandler.LoadProfile();
                if (selectedProfile.savedGameData != null)
                    gameData = selectedProfile.savedGameData;
            }
        }
    }
    public void SaveProfile()
    {
        foreach (ISave savable in saveableObjects)
        {
            savable.SaveData(ref gameData);
        }
        profileHandler.SaveProfile(selectedProfile);
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
        selectedProfile.savedGameData = gameData;
        profileHandler.SaveProfile(selectedProfile);
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
