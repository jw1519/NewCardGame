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
    public Button endTurnButton;

    public List<GameObject> combatOrder;
    public int currentCombatIndex = 0;
    public int cardsDrawn = 2;

    BasePanel gameWonPanel;


    private void Start()
    {
        gameWonPanel = AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("GameWonPanel");
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
        SetCharacterUI characterUI = player.GetComponent<SetCharacterUI>();
        character.defence = 0;
        characterUI.UpdateDefenceUI();
        character.UpdateEffect();
        characterUI.UpdateStatusEffectUI();
        character.energy = character.maxEnergy;
        characterUI.UpdateEnergyUI();
        //characterUI.UpdateStatusEffectUI();

        endTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "End Turn";
        endTurnButton.enabled = true;

        CardManager.instance.DrawCard(cardsDrawn);
    }
    public void EnemyTurn(GameObject enemy)
    {
        BaseEnemy character = enemy.GetComponent<SetEnemyUI>().enemy;
        if (character.isAlive != false)
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
            gameWonPanel.OpenPanel();
        }
    }
    public bool EnemiesAlive()
    {
        foreach (GameObject gameObject in combatOrder)
        {
            if (gameObject == null)
            {
                combatOrder.Remove(gameObject);
                continue;
            }
            if (gameObject.GetComponent<SetEnemyUI>() != null)
            {
                return true;
            }
        }
        return false;
    }
    public void ClearCombat()
    {
        foreach (GameObject gameObject in combatOrder)
        {
            if (gameObject == null)
            {
                combatOrder.Remove(gameObject);
                continue;
            }
            if (gameObject.GetComponent<SetEnemyUI>() != null)
            {
                Destroy(gameObject);
            }
        }

        combatOrder.Clear();
    }
}
