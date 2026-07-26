using Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{
    public int AmountOfCards;
    SetCharacterUI characterUI;
    [Header("Reroll")]
    public int rerollCost;
    public TextMeshProUGUI rerollText;
    public Button reRollButton;

    [Header("Restore Health")]
    int healthRestorCost;
    public TextMeshProUGUI healthRestoreText;
    public Button restoreHealthButton;


    private void Awake()
    {
        characterUI = AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>();
    }

    private void OnEnable()
    {
        rerollText.text = "Reroll " + rerollCost.ToString() + "g";
        UpdateShopUI();
        UpdateHealthRestoreUI();
        ShopManager.instance.SetUpShop();
    }
    private void OnDisable()
    {
        ShopManager.instance.ClearShop();
        GameManager.instance.RoomCleared();
        AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("MapPanel").OpenPanel();
    }
    public void UpdateShopUI()
    {
        if (characterUI.character.gold >= rerollCost)
        {
            reRollButton.interactable = true;
        }
        else
        {
            reRollButton.interactable = false;
        }
    }
    public void Reroll()
    {
        bool canReroll = ShopManager.instance.CanBuy(rerollCost);
        if (canReroll)
        {
            ShopManager.instance.ClearShop();
            ShopManager.instance.SetUpShop();
        }
    }
    public void RestoreHealth()
    {
        bool canBuy = ShopManager.instance.CanBuy(healthRestorCost);
        if (canBuy)
        {
            characterUI.character.health = characterUI.character.maxHealth;
            characterUI.gameObject.GetComponent<SetCharacterUI>().UpdateHealthUI();
            healthRestoreText.transform.parent.gameObject.SetActive(false);
        }
    }
    public void UpdateHealthRestoreUI()
    {
        healthRestorCost = characterUI.character.maxHealth - characterUI.character.health;
        if (healthRestorCost > 0)
        {
            restoreHealthButton.interactable = true;
            healthRestoreText.text = "Restore Health " + healthRestorCost.ToString() + "g";
        }
        else
        {
            restoreHealthButton.interactable = false;
            healthRestoreText.text = "Health Full";
        }
    }
}
