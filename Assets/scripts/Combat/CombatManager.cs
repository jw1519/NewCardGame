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
    CombatPanel combatPanel;

    public List<GameObject> combatOrder;
    int currentCombatIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        combatPanel = FindAnyObjectByType<CombatPanel>();
    }

    public void AddToCombat(GameObject character)
    {
        combatOrder.Add(character);
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
        else
        {
            Debug.Log("current combat index is null");
        }
    }
    public void PlayerTurn(GameObject player)
    {
        BaseCharacter character = player.GetComponent<BaseCharacter>();
        character.energy = character.maxEnergy;
        player.GetComponent<SetCharacterUI>().UpdateEnergyUI();

        endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "End Turn";
        endTurnButton.enabled = true;

        CardManager.instance.DrawCards();
    }
    public void EnemyTurn(GameObject enemy)
    {
        if (enemy.GetComponent<SetEnemyUI>().enemy.isAlive != false)
        {
            combatPanel.UpdateCombatOrder();
            endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy Turn";
            endTurnButton.enabled = false;
            enemy.GetComponent<EnemyCombat>().StartTurn();
            combatPanel.UpdateCombatOrder();
        }
    }
    public void RemoveFromCombat(GameObject character)
    {
        combatOrder.Remove(character);
        combatPanel.RemoveFromCombat(character);
    }
}
