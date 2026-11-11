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
    public FileDataHandler profileHandler {  get; private set; }
    GameData gameData;

    List<ProfileData> profileData = new();
    public ProfileData selectedProfile;

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
    public void LoadProf(string profileName)
    {
        if (profileData.Find(p => p.profileName == profileName) == null)
        {
            NewProfile(profileName);
            return;
        }
        profileHandler = new FileDataHandler(Application.persistentDataPath, "Profile " + LoadProfile.SelectedProfile);
        dataHandler = new FileDataHandler(Application.persistentDataPath, selectedProfile.profileName + " game");
        if (selectedProfile.profileName == profileName)
        {
            UIManager.instance.GetPanel("ProfileSelectPanel").ClosePanel();
            return;
        }
        selectedProfile = profileHandler.LoadProfile();
        foreach (ProfileData data in profileData)
        {
            if (data.profileName == profileName)
            {
                selectedProfile = profileHandler.LoadProfile();
                if (selectedProfile.savedGameDataName != null)
                {
                    gameData = new FileDataHandler(Application.persistentDataPath, data.profileName + " game").Load();
                    Debug.Log(gameData);
                }
            }
        }
    }
    public void SaveProfile()
    {
        profileHandler = new FileDataHandler(Application.persistentDataPath, selectedProfile.profileName);
        selectedProfile.savedGameDataName = selectedProfile.profileName + " game";
        profileHandler.SaveProfile(selectedProfile);
    }
    public void NewGame()
    {
        gameData = new GameData();
    }
    public void LoadGame()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, selectedProfile.profileName + " game");
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
        dataHandler = new FileDataHandler(Application.persistentDataPath, selectedProfile.profileName + " game");
        dataHandler.Save(gameData);
        SaveProfile();
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
