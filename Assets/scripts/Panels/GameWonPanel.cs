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
    }
    private void OnEnable()
    {
        UpdateStats();
    }
    private void OnDisable()
    {
        characterUI.character.GainGold(goldEarned);
        characterUI.gameObject.GetComponent<SetCharacterUI>().UpdateGoldUI();
        goldEarned = 0;
        GameManager.instance.RoomCleared();
        GameManager.instance.EndRound();
        AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("MapPanel").OpenPanel();
    }
    public void UpdateStats()
    {
        goldEarnedText.text = "Gold Earned " + goldEarned.ToString();
        if (characterUI == null) return;
    }
    public void UpdateGold(int goldAmount)
    {
        Debug.Log(goldAmount);
        goldEarned += goldAmount;
    }
}
