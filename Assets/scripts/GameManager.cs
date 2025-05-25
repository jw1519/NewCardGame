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
    public static GameManager instance;
    public Button endTurnButton;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        CardManager.instance.DrawCards();
    }

    public void PlayerTurn(SetCharacterUI character)
    {
        endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "End Turn";
        endTurnButton.enabled = true;

        CardManager.instance.DrawCards();
    }
    public void EndPlayerTurn()
    {
        CardManager.instance.DiscardCards();
        EnemyTurn();
    }
    public void EnemyTurn()
    {
        endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy Turn";
        endTurnButton.enabled = false;

        StartCoroutine(CombatManager.instance.StartCombat());
    }
    public void EndEnemyTurn()
    {

    }
}
