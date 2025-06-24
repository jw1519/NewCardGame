using System.Collections.Generic;
using UnityEngine;
using Card;
using Character;
using Item;
using static Card.CardPack;
using System;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    BaseCharacter character;

    [Header("Amounts")]
    public int maxCardPackAmount;
    public int maxItemAmount;

    [Header("Lists")]
    public List<BaseItem> items;
    public List<BaseCard> cards;

    [Header("Parent Transforms")]
    public Transform itemParent;
    public Transform cardPackParent;
    public Transform cardParent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        character = FindAnyObjectByType<BaseCharacter>();   
    }
    public void SetUpShop()
    {
        for (int i = 0; i < maxCardPackAmount; i++ )
        {
            GameObject instance = CardPackFactory.instance.CreateCardPack(GetRandomEnumValue<CardPackType>());
            instance.transform.SetParent(cardPackParent, false);
        }
        for (int i = 0; i < maxItemAmount; i++)
        {
            GameObject instance = ItemFactory.instance.CreateItem(items[UnityEngine.Random.Range(0, items.Count)]);
            instance.transform.SetParent(itemParent, false);
        }
    }
    public CardPackType GetRandomEnumValue<Action>()
    {
        Array enumvalues = Enum.GetValues(typeof(CardPackType)); // gets all posable values for the enum
        return (CardPackType)enumvalues.GetValue(UnityEngine.Random.Range(0, enumvalues.Length)); //randomly selcts on of the actions from the enum and returns it
    }
    public void ClearShop()
    {
        foreach (Transform child in itemParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in cardPackParent)
        {
            Destroy(child.gameObject);
        }
    }
    public void OpenCardPack(int amountOfCards)
    {
        for (int i = 0; i <= amountOfCards; i++)
        {
            int random = UnityEngine.Random.Range(0, cards.Count);
            GameObject instance = CardFactory.instance.CreateCard(cards[random]);
            instance.GetComponent<Hover>().enabled = false;
            instance.transform.SetParent(cardParent, false);
            instance.AddComponent<CardSelect>();
        }
        cardParent.gameObject.SetActive(true);
    }
    public void CardSelected()
    {
        foreach (Transform child in cardParent)
        {
            Destroy(child.gameObject);
        }
        cardParent.gameObject.SetActive(false);
    }
    public bool CanBuy(int cost)
    {
        if (character.gold >= cost)
        {
            character.gold -= cost;
            character.gameObject.GetComponent<SetCharacterUI>().UpdateGoldUI();
            return true;
        }
        Debug.Log("not enough gold");
        return false;
    }
}
