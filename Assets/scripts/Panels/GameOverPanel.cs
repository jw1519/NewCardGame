using Character;
using TMPro;
using UnityEngine;

public class GameOverPanel : BasePanel
{
    public int roundsWon;
    public int totalGoldEarned;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI goldEarnedText;
    public void PlayerStatsDisplay(BaseCharacter player)
    {
        roundText.text = "Rounds Won " + roundsWon.ToString();
        goldEarnedText.text = "Total Gold Earned " + player.totalGoldCollected.ToString();
    }
    public void NewRun()
    {
        GameManager.instance.NewRound();
        ClosePanel();
    }
}
