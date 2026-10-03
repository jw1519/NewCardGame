using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    GameData gameData;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }
    public void LoadGame()
    {
        if (gameData == null)
        {
            Debug.LogError("GameData is null. Cannot load game.");
            NewGame();
        }
    }
    public void SaveGame()
    {

    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
