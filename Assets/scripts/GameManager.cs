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
    public void EndPlayerTurn()
    {
        CardManager.instance.DiscardAllCards();
        StartCoroutine(CombatManager.instance.StartCombat());
    }
}
