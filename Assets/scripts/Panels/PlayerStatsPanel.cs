using TMPro;

public class PlayerStatsPanel : BasePanel
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI roundText;
    public void UpdatePlayerHealthUI(int health, int maxHealth)
    {
        healthText.text = health + "/" + maxHealth.ToString();
    }
    public void UpdateGoldUI(int goldAmount)
    {
        goldText.text = goldAmount.ToString() + "g";
    }
    public void UpdateRoundUI(int round)
    {
        roundText.text = "Round " + round.ToString();
    }

}
