using Character;
using TMPro;
using UnityEngine;

public class GameOverPanel : BasePanel
{
    public int roundsWon;
    public int totalGoldEarned;

    public TextMeshProUGUI roundText;
    public TextMeshProUGUI goldEarnedText;
    public TextMeshProUGUI highScoreText;

    int highScore;
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
    }
    public void PlayerStatsDisplay(BaseCharacter player)
    {
        roundsWon = GameManager.instance.round--;
        roundText.text = "Rounds Won " + roundsWon.ToString();
        goldEarnedText.text = "Total Gold Earned " + player.totalGoldCollected.ToString();
        UpdateHighScore();

        highScoreText.text = "Most Rounds Won " + highScore.ToString();
    }
    public void NewRun()
    {
        roundsWon = 0;
        totalGoldEarned = 0;
        GameManager.instance.NewRun();
        ClosePanel();
    }

    public void UpdateHighScore()
    {
        if (roundsWon > highScore)
        {
            highScore = roundsWon;
            PlayerPrefs.SetInt("hightScore", highScore );
        }
    }
}
