using TMPro;

public class ShopPanel : BasePanel
{
    public int AmountOfCards;

    public int rerollCost;
    public TextMeshProUGUI rerollText;


    private void OnEnable()
    {
        ShopManager.instance.SetUpShop();
        rerollText.text = "Reroll " + rerollCost.ToString() + "g";
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


}
