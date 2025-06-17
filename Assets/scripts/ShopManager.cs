using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;
using Character;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    BaseCharacter character;

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
        foreach (BaseCard card in items)
        {
            GameObject instance = CardFactory.instance.CreateCard(card);
            instance.transform.SetParent(itemParent, false);
        }
    }
    public void ClearShop()
    {
        foreach (Transform child in itemParent)
        {
            Destroy(child);
        }
    }

    public void Buy(int cost)
    {
        if (character.gold >= cost)
        {

        }
        else
        {
            Debug.Log("not enough gold");
        }
    }
}
