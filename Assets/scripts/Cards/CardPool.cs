using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class CardPool : MonoBehaviour
    {
        public static CardPool instance;
        public List<GameObject> pooledCards;
        public List<BaseCard> cardSO = new();

        public GameObject cardToPool;
        public Transform cardParent;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            SetUp();
        }
        public void SetUp()
        {
            foreach (BaseCard card in cardSO)
            {
                AddCardToPool(Instantiate(card));
            }
        }
        public GameObject GetPooledCard()
        {
            List<GameObject> cardDeck = CardManager.instance.cardsInDeck;
            GameObject randomCard = cardDeck[Random.Range(0, cardDeck.Count)];
            return randomCard;
        }
        public GameObject GetCard(BaseCard card)
        {
            foreach (GameObject pooledCard in pooledCards)
            {
                if (pooledCard.GetComponent<SetCardUI>().card.cardName == card.cardName)
                {
                    return pooledCard;
                }
            }
            return null;
        }
        public void AddCardToPool(BaseCard card)
        {
            GameObject newCard = AssetManager.Instance.GetAsset("CardFactory").GetComponent<CardFactory>().CreateCard(card);
            newCard.transform.SetParent(cardParent);
            pooledCards.Add(newCard);
            CardManager.instance.cardsInDeck.Add(newCard);
        }
        public void RemoveCardFromPool(BaseCard card)
        {
            GameObject cardToRemove = GetCard(card);
            if (cardToRemove != null)
            {
                pooledCards.Remove(cardToRemove);
                CardManager.instance.cardsInDeck.Remove(cardToRemove);
                Destroy(cardToRemove);
            }
        }
    }
}