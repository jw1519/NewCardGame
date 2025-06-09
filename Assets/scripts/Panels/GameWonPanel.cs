using Character;
using TMPro;
public class GameWonPanel : BasePanel
{
    public int goldEarned;
    public TextMeshProUGUI goldEarnedText;

    private void OnEnable()
    {
        UpdateStats();
    }
    public void UpdateStats()
    {
        goldEarnedText.text = "Gold Earned " + goldEarned.ToString();
        FindObjectOfType<BaseCharacter>().gold += goldEarned;
    }
    public void NextRound()
    {
        FindFirstObjectByType<BaseCharacter>().gold += goldEarned;
        FindFirstObjectByType<BaseCharacter>().totalGoldCollected += goldEarned;
        ClosePanel();
        GameManager.instance.NewRound();
    }

}
