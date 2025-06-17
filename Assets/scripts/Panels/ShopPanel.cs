using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;

public class ShopPanel : BasePanel
{
    public int AmountOfCards;


    private void OnEnable()
    {
        ShopManager.instance.SetUpShop();
    }
    private void OnDisable()
    {
        ShopManager.instance.ClearShop();
        GameManager.instance.NewRound();
    }
    public void Reroll()
    {
        ShopManager.instance.ClearShop();
        ShopManager.instance.SetUpShop();
    }


}
