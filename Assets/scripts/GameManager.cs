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

    public List<GameObject> combatOrder;

    public Button endTurnButton;

    private void Start()
    {
        CardManager.instance.DrawCards();
    }

    public void PlayerTurn(BaseCharacter character)
    {
        endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "End Turn";
        endTurnButton.enabled = true;

        CardManager.instance.DrawCards();
    }
    public void EnemyTurn(BaseEnemy enemy)
    {
        endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy Turn";
        endTurnButton.enabled = false;
    }
}
