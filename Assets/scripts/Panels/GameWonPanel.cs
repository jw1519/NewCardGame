using Character;
using Enemy;
using TMPro;
using UnityEngine;
public class GameWonPanel : BasePanel
{
    public int goldEarned;
    public TextMeshProUGUI goldEarnedText;

    BaseCharacter player;

    private void Awake()
    {
        player = FindAnyObjectByType<BaseCharacter>();
        BaseEnemy.enemydiedGold += UpdateGold;
    }
    private void OnEnable()
    {
        UpdateStats();
    }
    private void OnDisable()
    {
        goldEarned = 0;
        GameManager.instance.RoomCleared();
        GameManager.instance.EndRound();
        AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("MapPanel").OpenPanel();
    }
    public void UpdateStats()
    {
        goldEarnedText.text = "Gold Earned " + goldEarned.ToString();
        if (player == null) return;
        player.gold += goldEarned;
        player.totalGoldCollected += goldEarned;
        player.gameObject.GetComponent<SetCharacterUI>().UpdateGoldUI();
    }
    public void UpdateGold(int goldAmount)
    {
        goldEarned += goldAmount;
    }
}
