using UnityEngine;
using Enemy;
using System.Collections.Generic;
using Character;

namespace Card
{
    public class SelectManager : MonoBehaviour
    {
        public List<BaseCharacter> characterList;
        [HideInInspector] public SetCardUI cardSelected;
        CardManager cardManager;
        CardHand cardHand;

        private void Start()
        {
            cardManager = AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>();
            cardHand = AssetManager.Instance.GetAsset("CardHand").GetComponent<CardHand>();
        }
        public void SelectCard(GameObject card)
        {
            if (cardSelected != null)
            {
                DeselectCard();
            }
            cardSelected = card.GetComponent<SetCardUI>();
        }
        public void DeselectCard()
        {
            if (cardSelected != null)
            {
                cardSelected.GetComponent<UseCard>().DeselectCard();
                cardSelected = null;
                StartCoroutine(cardHand.UpdateCardPositions(0));
            }
        }
        public void UseCard(GameObject target)
        {
            if (cardSelected != null)
            {
                if (cardSelected.card.cardType == BaseCard.CardType.Attack && target.GetComponent<SetEnemyUI>() == null)
                {
                    Debug.Log("Select Enemy");
                    return;
                }
                else if (cardSelected.card.cardType == BaseCard.CardType.Defence && target.GetComponent<SetCharacterUI>() == null)
                {
                    Debug.Log("Select Character");
                    return;
                }
                
                if (cardSelected != null)
                {
                    cardHand.cards.Remove(cardSelected.gameObject);
                    cardManager.DiscardCard(cardSelected.gameObject);
                }
                cardSelected.card.Use(target);
                StartCoroutine(cardHand.UpdateCardPositions(0.15f));
                
                cardSelected = null;
            }
        }
    }
}
