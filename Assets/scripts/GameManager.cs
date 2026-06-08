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
    public int round;
    public static event Action<int> updateRounds;

    [Header("enemy")]
    public List<BaseEnemy> normalEnemies;
    public List<BaseEnemy> eliteEnemies;
    public Transform enemyParent;
    int maxEnemyAmount = 3;

    SetCharacterUI player;
    CombatManager combatManager;
    BaseRoom currentRoom;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        round = 0;
        player = FindAnyObjectByType<SetCharacterUI>();
        combatManager = AssetManager.Instance.GetAsset("CombatManager").GetComponent<CombatManager>();
    }
    public void SetRoom(BaseRoom room)
    {
        if (room != null)
        {
            currentRoom = room;
        }
        else
        {
            currentRoom = null;
        }
    }
    public void RoomCleared()
    {
        Events.OnRoomCleared(currentRoom.x, currentRoom.y);
    }
    public void EndPlayerTurn()
    {
        StartCoroutine(combatManager.StartCombat());
    }
    public void NewRound()
    {
        round++;
        updateRounds?.Invoke(round);
        CardManager.instance.EmptyDiscardPile();
        player.character.energy = player.character.maxEnergy;
        player.UpdateEnergyUI();
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
        round = 0;
        combatManager.ClearCombat();
        player.NewRun();
        NewRound();
    }
}
