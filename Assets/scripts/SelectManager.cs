using UnityEngine;
using Enemy;
using System.Collections.Generic;
using Character;

namespace Card
{
    public class SelectManager : MonoBehaviour
    {
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
                if (cardSelected.card.usedOnEnemy && target.GetComponent<SetEnemyUI>() == null) return;
                if (!cardSelected.card.usedOnEnemy && target.GetComponent<SetCharacterUI>() == null) return;

                cardHand.cards.Remove(cardSelected.gameObject);
                if (cardSelected.card.isSingleUse)
                {
                    cardManager.AddDeadCard(cardSelected.gameObject);
                }
                else
                {
                    cardManager.DiscardCard(cardSelected.gameObject);
                }
            }
            cardSelected.card.Use(target);
            StartCoroutine(cardHand.UpdateCardPositions(0.15f));

            cardSelected = null;
        }
    }
}
