using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;
using Enemy;
using Character;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    [HideInInspector] public BaseCard cardSelected;

    public void SelectCard(BaseCard card, BaseCharacter character)
    {
        if (character.energy - card.cardEnergy >= 0 )
        {
            cardSelected = card;
        }
        else
        {
            Debug.Log("not enough energy");
        }
    }
}
