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



    public Button endTurnButton;

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
    }
    public void EnemyTurn(GameObject enemy)
    {
        endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy Turn";
        endTurnButton.enabled = false;

        //enemy.GetComponent<EnemyTurn>().StartTurn();
    }
}
