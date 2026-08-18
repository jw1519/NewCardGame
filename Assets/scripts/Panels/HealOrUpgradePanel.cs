using Character;
using UnityEngine;

public class HealOrUpgradePanel : BasePanel
{
    public void HealPlayer()
    {
        BaseCharacter character = AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>().character;
        character.Heal(character.maxHealth / 3);
        ClosePanel();
        GameManager.instance.RoomCleared();
    }
    public void UpgradeCard()
    {
        ClosePanel();
        GameManager.instance.RoomCleared();
        Debug.Log("Upgrade Card");
        //AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("CardUpgradePanel").GetComponent<UpgradeCardPanel>().OpenPanel();
    }
}
