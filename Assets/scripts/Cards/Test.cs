using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public BaseCard card;
    public CardHand hand;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject cardGO = CardFactory.instance.CreateCard(card, transform.position, Quaternion.identity);
        cardGO.transform.SetParent(hand.transform);
        StartCoroutine(hand.AddCard(cardGO));

        GameObject cardGO1 = CardFactory.instance.CreateCard(card, transform.position, Quaternion.identity);
        cardGO.transform.SetParent(hand.transform);
        StartCoroutine(hand.AddCard(cardGO1));

        GameObject cardGO2 = CardFactory.instance.CreateCard(card, transform.position, Quaternion.identity);
        cardGO.transform.SetParent(hand.transform);
        StartCoroutine(hand.AddCard(cardGO2));

        GameObject cardGO3 = CardFactory.instance.CreateCard(card, transform.position, Quaternion.identity);
        cardGO.transform.SetParent(hand.transform);
        StartCoroutine(hand.AddCard(cardGO3));

        GameObject cardGO4 = CardFactory.instance.CreateCard(card, transform.position, Quaternion.identity);
        cardGO.transform.SetParent(hand.transform);
        StartCoroutine(hand.AddCard(cardGO4));
    }

}
