using Character;
using TMPro;
using UnityEngine;

public class ShopPanel : BasePanel
{
    public int AmountOfCards;
    BaseCharacter character;
    [Header("Reroll")]
    public int rerollCost;
    public TextMeshProUGUI rerollText;

    [Header("Reastore Health")]
    int healthRestorCost;
    public TextMeshProUGUI healthRestoreText;

    private void Awake()
    {
        character = FindAnyObjectByType<BaseCharacter>();
    }

    private void OnEnable()
    {
        rerollText.text = "Reroll " + rerollCost.ToString() + "g";
        UpdateHealthRestoreUI();
        ShopManager.instance.SetUpShop();
    }
    private void OnDisable()
    {
        ShopManager.instance.ClearShop();
        GameManager.instance.NewRound();
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
            character.health = character.maxHealth;
            character.gameObject.GetComponent<SetCharacterUI>().UpdateHealthUI();
        }
    }
    public void UpdateHealthRestoreUI()
    {
        healthRestorCost = character.maxHealth - character.health;
        if (healthRestorCost > 0)
        {
            healthRestoreText.transform.parent.gameObject.SetActive(true);
            healthRestoreText.text = "Restore Health " + healthRestorCost.ToString() + "g";
        }
        else
        {
            healthRestoreText.transform.parent.gameObject.SetActive(false);
        }   
    }
}
