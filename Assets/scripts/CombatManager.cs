using Card;
using Character;
using Enemy;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public Button endTurnButton;

    public List<GameObject> combatOrder;
    int currentCombatIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator StartCombat()
    {

        yield return new WaitForSeconds(1f);

        if (combatOrder[currentCombatIndex] != null)
        {
            GameObject Target = combatOrder[currentCombatIndex];
            if (Target.GetComponent<SetEnemyUI>() != null)
            {
                EnemyTurn(Target);
            }
            else
            {
                PlayerTurn(Target);
            }
            currentCombatIndex = (currentCombatIndex + 1) % combatOrder.Count; // moves to the next in the list and wraps around

        }
    }
    public void PlayerTurn(GameObject player)
    {
        endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "End Turn";
        endTurnButton.enabled = true;

        CardManager.instance.DrawCards();
    }
    public void EnemyTurn(GameObject enemy)
    {
        endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy Turn";
        endTurnButton.enabled = false;

        enemy.GetComponent<EnemyTurn>().StartTurn();
    }

}
