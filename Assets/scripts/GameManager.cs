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
    public List<BaseEnemy> enemyList;
    public Transform enemyParent;
    int maxEnemyAmount = 3;

    SetCharacterUI player;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        round = 0;
        player = FindFirstObjectByType<SetCharacterUI>();
        NewRound();
    }
    public void EndPlayerTurn()
    {
        CardManager.instance.DiscardAllCards();
        StartCoroutine(CombatManager.instance.StartCombat());
    }
    public void NewRound()
    {
        round++;
        updateRounds?.Invoke(round);
        CardManager.instance.EmptyDiscardPile();
        player.character.energy = player.character.maxEnergy;
        player.UpdateEnergyUI();
        EndRound();
        int enemyAmount = UnityEngine.Random.Range(1, maxEnemyAmount);
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject instance = EnemyFactory.instance.CreateEnemy(RandomEnemy());
            instance.transform.SetParent(enemyParent);
        }
        CombatManager.instance.currentCombatIndex = 0;
        StartCoroutine(CombatManager.instance.StartCombat());
    }
    public void EndRound()
    {
        foreach (Transform transform in enemyParent)
        {
            Destroy(transform.gameObject);
        }
    }
    public BaseEnemy RandomEnemy()
    {
        if (enemyList.Count > 1)
        {
            int random = UnityEngine.Random.Range(0,enemyList.Count);
            return enemyList[random];
        }
        else
        {
            return enemyList[0];
        }
    }
}
