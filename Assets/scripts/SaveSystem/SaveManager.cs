using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static bool loadOnStart = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    public static SaveManager Instance { get; private set; }

    public List<ISave> saveableObjects = new List<ISave>();
    FileDataHandler dataHandler;
    GameData gameData;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        saveableObjects = FindAllSavableObjects();
        if (loadOnStart)
        {
            LoadGame();
            UIManager uiManager = AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>();
            uiManager.CloseAllPanels();
            uiManager.GetPanel("PlayerStatsPanel").OpenPanel();
            uiManager.GetPanel("MapPanel").OpenPanel();
        }
        else
        {
            NewGame();
        }
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
        if (!gameData.enemiesFound.Contains(enemyName))
            gameData.enemiesFound.Add(enemyName);
    }
}
