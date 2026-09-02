using UnityEngine;
using Enemy;
using Character;

namespace Card
{
    public class SelectManager : MonoBehaviour
    {
        public SetCardUI cardSelected;
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
                DeselectCard(card);
            }
            cardSelected = card.GetComponent<SetCardUI>();
            cardSelected.GetComponent<UseCard>().isSelected = true;
            cardSelected.GetComponent<Hover>().enabled = false;
        }
        public void DeselectCard(GameObject card = null)
        {
            if (cardSelected != null)
            {
                UseCard useCard = cardSelected.GetComponent<UseCard>();
                useCard.isSelected = false;
                useCard.discardButton.SetActive(false);
                useCard.gameObject.GetComponent<Hover>().enabled = true;

                if (card != null)
                {
                    card.transform.SetParent(cardHand.transform);
                    cardSelected = card.GetComponent<SetCardUI>();
                    cardSelected.GetComponent<Hover>().HoverCard();
                }
                else
                {
                    cardSelected = null;
                }
                useCard.gameObject.GetComponent<Hover>().ResetCard();
            }
            cardHand.StartCoroutine(cardHand.UpdateCardPositions(0));
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
                cardSelected.card.Use(target);
                StartCoroutine(cardHand.UpdateCardPositions(0.15f));

                cardSelected = null;
            }
             


        }
    }
}
