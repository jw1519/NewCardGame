using UnityEngine;
using UnityEngine.UI;
using Card;

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
