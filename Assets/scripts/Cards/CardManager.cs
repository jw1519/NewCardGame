using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class CardManager : MonoBehaviour
    {
        public static CardManager instance;
        CardHand hand;

        [Header("Card Lists")]
        public List<GameObject> cardsInDeck;
        public List<GameObject> cardsInHand;
        public List<GameObject> cardsInDiscard;

        [Header("Card Parents")]
        public Transform discardedCardParent;
        public Transform deckCardParent;

        public int startingCardsInHand;
        public int maxCardsInHand;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            hand = FindFirstObjectByType<CardHand>();
        }
        public void DrawCards()
        {
            DiscardAllCards();
            if (cardsInDeck.Count >= startingCardsInHand)
            {
                for (int i = 0; i < startingCardsInHand; i++)
                {
                    GameObject RandomCard = CardPool.instance.GetPooledCard();
                    if (RandomCard != null)
                    {
                        RandomCard.gameObject.SetActive(true);
                        RandomCard.transform.SetParent(hand.transform, false);
                        cardsInHand.Add(RandomCard);
                        StartCoroutine(hand.AddCard(RandomCard));
                        cardsInDeck.Remove(RandomCard);
                        RandomCard.GetComponent<Hover>().enabled = true;
                        RandomCard.GetComponent<DragAndDrop>().enabled = true;
                    }
                }
            }
            else
            {
                // put cards from discard pile into deck if deck is empty or doesnt have enough cards
                foreach (GameObject card in cardsInDiscard)
                {
                    cardsInDeck.Add(card);
                    card.transform.SetParent(deckCardParent, false);
                }
                cardsInDiscard.Clear();
                DrawCards();
            }
        }
        //discard any unused cards when turn ends
        public void DiscardAllCards()
        {
            // add all cards in hand to discard list
            foreach (GameObject card in cardsInHand)
            {
                cardsInDiscard.Add(card);
            }
            // discard all cards in hand
            for (int i = hand.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = hand.transform.GetChild(i);
                child.SetParent(discardedCardParent, false);
            }
            //discard any cards left in lists
            cardsInHand.Clear();
            hand.cards.Clear();
        }
        public void DiscardCard(GameObject card)
        {
            card.transform.SetParent(discardedCardParent, false);
            cardsInHand.Remove(card);
        }
        public void AddCard(GameObject card)
        {
            cardsInDeck.Add(card);
            card.transform.SetParent(deckCardParent);
        }
    }
}
