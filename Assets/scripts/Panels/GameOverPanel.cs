using Character;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel
{
    public int roomsCleared;
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
        roomsCleared = GameManager.instance.roomsCleared--;
        roundText.text = "Rooms cleared " + roomsCleared.ToString();
        goldEarnedText.text = "Total Gold Earned " + player.totalGoldCollected.ToString();
        UpdateHighScore();

        highScoreText.text = "Most rooms cleared " + highScore.ToString();
    }
    public void NewRun()
    {
        roomsCleared = 0;
        totalGoldEarned = 0;
        GameManager.instance.NewRun();
        ClosePanel();
    }

    public void UpdateHighScore()
    {
        if (roomsCleared > highScore)
        {
            highScore = roomsCleared;
            PlayerPrefs.SetInt("hightScore", highScore );
        }
    }
    public void MainMenu()
    {
        ClosePanel();
        SceneManager.LoadScene("Menu");
    }
}
