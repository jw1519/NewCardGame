using UnityEngine;
using UnityEngine.UI;
using Card;
using Enemy;
using Character;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Button endTurnButton;
    public int roomsCleared;
    public static event Action<int> updateRoomsCleared;

    [Header("enemy")]
    public List<BaseEnemy> normalEnemies;
    public List<BaseEnemy> eliteEnemies;
    public Transform enemyParent;
    public List<Transform> enemyPositions;
    int maxEnemyAmount = 3;

    SetCharacterUI player;
    CombatManager combatManager;
    BaseRoom currentRoom;
    MapPanel mapPanel;
    EnemyFactory enemyFactory;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        foreach (Transform child in enemyParent)
        {
            enemyPositions.Add(child);
        }
        roomsCleared = 0;
        player = FindAnyObjectByType<SetCharacterUI>();
        combatManager = AssetManager.Instance.GetAsset("CombatManager").GetComponent<CombatManager>();
        mapPanel = AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("MapPanel").GetComponent<MapPanel>();
        enemyFactory = AssetManager.Instance.GetAsset("EnemyFactory").GetComponent<EnemyFactory>();
    }
    public void SetRoom(BaseRoom room)
    {
        if (room != null)
        {
            currentRoom = room;
            mapPanel.canClosePanel = true;
        }
        else
        {
            currentRoom = null;
        }
    }
    public void RoomCleared()
    {
        Events.OnRoomCleared(currentRoom.x, currentRoom.y);
        mapPanel.canClosePanel = false;
        roomsCleared++;
        updateRoomsCleared?.Invoke(roomsCleared);
        mapPanel.OpenPanel();
    }
    public void EndPlayerTurn()
    {
        StartCoroutine(combatManager.StartCombat());
    }
    public void StartCombat(string roomType)
    {
        CardManager.instance.DiscardAllCards();
        CardManager.instance.EmptyDiscardPile();
        player.character.energy = player.character.maxEnergy;
        player.UpdateEnergyUI();
        EndRound();
        CardManager.instance.NewRound();

        if (roomType == "Normal")
        {
            int enemyAmount = UnityEngine.Random.Range(1, maxEnemyAmount);
            for (int i = 0; i < enemyAmount; i++)
            {
                GameObject instance = enemyFactory.CreateEnemy(RandomEnemy());
                instance.transform.SetParent(enemyPositions[i], true);
                instance.transform.localPosition = Vector3.zero;
            }
        }
        else if (roomType == "Boss")
        {
            if (eliteEnemies.Count < 1) return;

            Debug.Log("Creating Elite Enemy");

            int i = UnityEngine.Random.Range(0, eliteEnemies.Count);
            GameObject instance = enemyFactory.CreateEnemy(eliteEnemies[i]);
            instance.transform.SetParent(enemyPositions[1], true);
            instance.transform.localPosition = Vector3.zero;
        }


        combatManager.currentCombatIndex = 0;
        StartCoroutine(combatManager.StartCombat());
    }
    public void AddEnemyToCombat(GameObject enemy)
    {
        enemy.transform.SetParent(enemyPositions.Find(pos => pos.childCount == 0), true);
        enemy.transform.localPosition = Vector3.zero;
    }
    public void EndRound()
    {
        foreach (Transform transform in enemyParent)
        {
            if (transform.childCount > 0)
            {
                GameObject instance = transform.GetChild(0).GetComponent<SetEnemyUI>().gameObject;
                if (instance != null)
                    Destroy(instance);
            }
            CardManager.instance.DiscardAllCards();
            CardManager.instance.ClearDeadCards();
        }
        player.character.RemoveAllEffects();
    }
    public BaseEnemy RandomEnemy()
    {
        if (normalEnemies.Count > 1)
        {
            int random = UnityEngine.Random.Range(0,normalEnemies.Count);
            return normalEnemies[random];
        }
        else
        {
            return normalEnemies[0];
        }
    }
    public void NewRun()
    {
        roomsCleared = 0;
        updateRoomsCleared?.Invoke(roomsCleared);
        combatManager.ClearCombat();
        player.NewRun();
        AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().CloseAllPanels();
        AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("PlayerStatsPanel").GetComponent<PlayerStatsPanel>().OpenPanel();
        mapPanel.CreateNewMap();
        mapPanel.canClosePanel = false;
        mapPanel.OpenPanel();
    }
}
