using Character;
using Enemy;
using TMPro;
using UnityEngine;
public class GameWonPanel : BasePanel
{
    public int goldEarned;
    public TextMeshProUGUI goldEarnedText;

    SetCharacterUI characterUI;

    private void Awake()
    {
        characterUI = AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>();
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
        if (characterUI == null) return;
        characterUI.character.gold += goldEarned;
        characterUI.character.totalGoldCollected += goldEarned;
        characterUI.gameObject.GetComponent<SetCharacterUI>().UpdateGoldUI();
    }
    public void UpdateGold(int goldAmount)
    {
        goldEarned += goldAmount;
    }
    public override void ClosePanel()
    {
        base.ClosePanel();
        AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>().character.RemoveAllEffects();
    }
}
