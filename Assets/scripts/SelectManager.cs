using UnityEngine;
using Enemy;
using Character;
using DG.Tweening;

namespace Card
{
    public class SelectManager : MonoBehaviour
    {
        public SetCardUI cardSelected;
        CardManager cardManager;
        CardHand cardHand;

        public Transform useCardParent;
        Vector2 anchoredPosition;

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
            cardSelected.transform.SetParent(useCardParent, true);
            anchoredPosition = card.GetComponent<RectTransform>().anchoredPosition;
            cardSelected.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.3f);
        }
        public void DeselectCard(GameObject card = null)
        {
            if (cardSelected != null)
            {
                UseCard useCard = cardSelected.GetComponent<UseCard>();
                useCard.isSelected = false;
                useCard.discardButton.SetActive(false);
                useCard.gameObject.GetComponent<Hover>().enabled = true;
                useCard.gameObject.transform.SetParent(cardHand.transform, true);
                cardSelected.GetComponent<RectTransform>().DOAnchorPos(anchoredPosition, 0.3f);

                if (card == null)
                {
                    cardSelected = null;
                }
                //useCard.gameObject.GetComponent<Hover>().ResetCard();
            }
            //cardHand.StartCoroutine(cardHand.UpdateCardPositions(0));
        }
        public void UseCard(GameObject target)
        {
            if (cardSelected != null)
            {
                if (AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>().character.energy - cardSelected.card.cardEnergy < 0)
                {
                    Debug.Log("not enough energy");
                    return;
                }
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
                cardHand.UpdateCards();

                cardSelected = null;
            }
             


        }
    }
}
