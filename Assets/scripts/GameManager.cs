using UnityEngine;
using UnityEngine.UI;
using Card;
using Enemy;
using Character;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Button endTurnButton;
    public int round;

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
    public void CheckCombatStatus()
    {
        bool combatActive = CombatManager.instance.EnemiesAlive();
        if (combatActive == false)
        {
            UIManager.instance.panelList.Find(panel => panel.name == "GameWonPanel").OpenPanel();
        }
    }
    public void NewRound()
    {
        round++;
        player.character.energy = player.character.maxEnergy;
        player.UpdateEnergyUI();
        GameObject panel = UIManager.instance.panelList.Find(panel => panel.name == "PlayerStatsPanel").gameObject;
        panel.GetComponent<PlayerStatsPanel>().UpdateRoundUI(round);
        EndRound();
        int enemyAmount = Random.Range(1, maxEnemyAmount);
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject instance = EnemyFactory.instance.CreateEnemy(RandomEnemy());
            instance.transform.SetParent(enemyParent);
        }
        CardManager.instance.DrawCards();
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
            int random = Random.Range(0,enemyList.Count);
            return enemyList[random];
        }
        else
        {
            return enemyList[0];
        }
    }
}
