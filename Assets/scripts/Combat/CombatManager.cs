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
        character.defence = 0;
        player.GetComponent<SetCharacterUI>().UpdateDefenceUI();
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
            endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy Turn";
            endTurnButton.enabled = false;
            enemy.GetComponent<EnemyCombat>().StartTurn();
        }
    }
    public void RemoveFromCombat(GameObject enemy)
    {
        combatOrder.Remove(enemy.gameObject);
        //check combat status
        bool isenemyAlive = EnemiesAlive();
        if (isenemyAlive == false)
        {
            UIManager.instance.panelList.Find(panel => panel.name == "GameWonPanel").OpenPanel();
        }
    }
    public bool EnemiesAlive()
    {
        foreach (GameObject gameObject in combatOrder)
        {
            if (gameObject.GetComponent<SetEnemyUI>() != null)
            {
                return true;
            }
        }
        return false;
    }
}
