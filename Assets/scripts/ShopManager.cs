using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;
using Character;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    BaseCharacter character;

    public int maxCardPackAmount;
    public int maxItemAmount;

    public List<BaseCard> items;
    public List<CardPack> cardPacks;

    public Transform itemParent;
    public Transform cardPackParent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        character = FindAnyObjectByType<BaseCharacter>();   
    }
    public void SetUpShop()
    {
        for (int i = 0; i <= maxCardPackAmount; i++ )
        {
            
        }
    }
    public void ClearShop()
    {
        foreach (Transform child in itemParent)
        {
            Destroy(child);
        }
        foreach (Transform child in cardPackParent)
        {
            Destroy(cardPackParent);
        }
    }

    public bool CanBuy(int cost)
    {
        if (character.gold >= cost)
        {
            return true;
        }
        Debug.Log("not enough gold");
        return false;
    }

    public void OpenCardPack()
    {

    }
}
