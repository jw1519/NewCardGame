using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Card;
using Character;
using Enemy;

public class GameManager : MonoBehaviour
{
    public List<BaseCharacter> characterList;
    public List<BaseEnemy> enemyList;

    public Button endTurnButton;

    private void Start()
    {
        PlayerTurn();
    }

    public void PlayerTurn()
    {
        endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "End Turn";
        endTurnButton.enabled = true;

        CardManager.instance.DrawCards();
    }
    public void EnemyTurn()
    {
        endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy Turn";
        endTurnButton.enabled = false;
    }
}
