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
            //DiscardCards();
            if (cardsInDeck.Count >= startingCardsInHand)
            {
                for (int i = 0; i < startingCardsInHand; i++)
                {
                    GameObject RandomCard = CardPool.instance.GetPooledCard();
                    if (RandomCard != null)
                    {
                        RandomCard.gameObject.SetActive(true);
                        RandomCard.transform.SetParent(hand.transform);
                        StartCoroutine(hand.AddCard(RandomCard));
                        cardsInDeck.Remove(RandomCard);
                    }
                }
            }
            else
            {
                // put cards from discard pile into deck if deck is empty or doesnt have enough cards
                foreach (GameObject card in cardsInDiscard)
                {
                    cardsInDeck.Add(card);
                }
                cardsInDiscard.Clear();
                DrawCards();
            }
        }
        //discard any unused cards when turn ends
        public void DiscardCards()
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
                child.SetParent(discardedCardParent);
                child.gameObject.SetActive(false);
            }
            //discard any cards left in lists
            cardsInHand.Clear();
            hand.cards.Clear();
        }

    }
}
