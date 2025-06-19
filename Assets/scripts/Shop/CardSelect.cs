using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    public void OnSelect()
    {
        CardManager.instance.cardsInDeck.Add(gameObject);
    }
}
