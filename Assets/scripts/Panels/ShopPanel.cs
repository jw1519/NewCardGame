using Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{
    public int AmountOfCards;
    BaseCharacter character;
    GameObject combatCanvas;
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
        character = FindAnyObjectByType<BaseCharacter>();
        combatCanvas = AssetManager.Instance.GetAsset("CombatCanvas");
    }

    private void OnEnable()
    {
        rerollText.text = "Reroll " + rerollCost.ToString() + "g";
        combatCanvas.SetActive(false);
        if (character.gold >= rerollCost)
        {
            reRollButton.interactable = true;
        }
        else
        {
            reRollButton.interactable = false;
        }
        UpdateHealthRestoreUI();
        ShopManager.instance.SetUpShop();
    }
    private void OnDisable()
    {
        ShopManager.instance.ClearShop();
        AssetManager.Instance.GetAsset("GameManager").GetComponent<GameManager>().NewRound();
        combatCanvas.SetActive(true);
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
            healthRestoreText.transform.parent.gameObject.SetActive(false);
        }
    }
    public void UpdateHealthRestoreUI()
    {
        healthRestorCost = character.maxHealth - character.health;
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
