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
    public Transform enemyParent;
    public List<Transform> enemyPositions;

    SetCharacterUI player;
    CombatManager combatManager;
    BaseRoom currentRoom;
    MapPanel mapPanel;
    CardManager cardManager;

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
        cardManager = AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>();
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
    public void StartCombat()
    {
        cardManager.DiscardAllCards();
        cardManager.EmptyDiscardPile();
        player.character.energy = player.character.maxEnergy;
        player.UpdateEnergyUI();
        cardManager.NewRound();
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
            cardManager.DiscardAllCards();
            cardManager.ClearDeadCards();
        }
        player.character.RemoveAllEffects();
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
