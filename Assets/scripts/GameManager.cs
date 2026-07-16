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
    int maxEnemyAmount = 3;

    SetCharacterUI player;
    CombatManager combatManager;
    BaseRoom currentRoom;
    MapPanel mapPanel;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        roomsCleared = 0;
        player = FindAnyObjectByType<SetCharacterUI>();
        combatManager = AssetManager.Instance.GetAsset("CombatManager").GetComponent<CombatManager>();
        mapPanel = AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("MapPanel").GetComponent<MapPanel>();
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
    public void NewRound()
    {
        CardManager.instance.EmptyDiscardPile();
        player.character.energy = player.character.maxEnergy;
        player.UpdateEnergyUI();
        //player.RemoveStatusEffects();
        EndRound();
        CardManager.instance.NewRound();
        int enemyAmount = UnityEngine.Random.Range(1, maxEnemyAmount);
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject instance = EnemyFactory.instance.CreateEnemy(RandomEnemy());
            instance.transform.SetParent(enemyParent);
        }
        combatManager.currentCombatIndex = 0;
        StartCoroutine(combatManager.StartCombat());
    }
    public void EndRound()
    {
        foreach (Transform transform in enemyParent)
        {
            Destroy(transform.gameObject);
            CardManager.instance.DiscardAllCards();
            CardManager.instance.ClearDeadCards();
        }
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
        combatManager.ClearCombat();
        player.NewRun();
        mapPanel.CreateNewMap();
        mapPanel.OpenPanel();
    }
}
