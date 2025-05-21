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
    }

}
