using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;

public class ShopPanel : BasePanel
{
    public List<BaseCard> cards;
    public int AmountOfCards;
    public Transform cardParent;

    private void OnEnable()
    {
        SetUpShop();
    }
    private void OnDisable()
    {
        ClearShop();
        GameManager.instance.NewRound();
    }
    public void Reroll()
    {
        ClearShop();
    }
    public void SetUpShop()
    {
        foreach (BaseCard card in cards)
        {
            GameObject instance = CardFactory.instance.CreateCard(card);
            instance.transform.SetParent(cardParent, false);
        }
    }
    public void ClearShop()
    {
        foreach (Transform child in cardParent)
        {
            Destroy(child);
        }
    }
}
