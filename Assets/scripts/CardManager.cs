using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

namespace Card
{
    public class CardManager : MonoBehaviour
    {
        public static CardManager instance;
        [HideInInspector] public BaseCard cardSelected;

        public void SelectCard(GameObject card, BaseCharacter character)
        {
            if (cardSelected == null)
            {
                if (character.energy - card.GetComponent<SetCardUI>().card.cardEnergy >= 0)
                {
                    cardSelected = card.GetComponent<SetCardUI>().card;
                    card.GetComponent<Hover>().enabled = false;

                }
                else
                {
                    Debug.Log("not enough energy");
                }
            }
            else
            {

                cardSelected = null;
                SelectCard(card, character);
            }

        }
        public void SelectEnemy()
        {

        }
    }
}
