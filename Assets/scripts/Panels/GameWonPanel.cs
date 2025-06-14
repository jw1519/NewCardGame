using Character;
using Enemy;
using TMPro;
public class GameWonPanel : BasePanel
{
    public int goldEarned;
    public TextMeshProUGUI goldEarnedText;

    BaseCharacter player;

    private void Awake()
    {
        player = FindObjectOfType<BaseCharacter>();
        BaseEnemy.enemydiedGold += UpdateGold;
        ClosePanel();
    }
    private void OnEnable()
    {
        UpdateStats();
    }
    private void OnDisable()
    {
        goldEarned = 0;
    }
    public void UpdateStats()
    {
        goldEarnedText.text = "Gold Earned " + goldEarned.ToString();
        player.gold += goldEarned;
        player.totalGoldCollected += goldEarned;
        player.gameObject.GetComponent<SetCharacterUI>().UpdateGoldUI();
    }
    public void UpdateGold(int goldAmount)
    {
        goldEarned += goldAmount;
    }
}
