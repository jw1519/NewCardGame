using UnityEngine;
using UnityEngine.UI;
using Card;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Button endTurnButton;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        CardManager.instance.DrawCards();
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
}
