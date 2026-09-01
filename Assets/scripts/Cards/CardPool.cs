using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace Card
{
    public class CardPool : MonoBehaviour
    {
        public static CardPool instance;
        public List<GameObject> pooledCards;
        public List<BaseCard> cardSO = new();

        public GameObject cardToPool;
        public Transform cardParent;
        public SplineContainer splineContainer;
        CardManager cardManager;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            cardManager = AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>();
            SetUp();
        }
        public void SetUp()
        {
            foreach (BaseCard card in cardSO)
            {
                AddAndCreateCardToPool(Instantiate(card));
            }
        }
        public GameObject GetPooledCard()
        {
            List<GameObject> cardDeck = cardManager.cardsInDeck;
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
        public void AddAndCreateCardToPool(BaseCard card)
        {
            GameObject newCard = AssetManager.Instance.GetAsset("CardFactory").GetComponent<CardFactory>().CreateCard(card);
            newCard.transform.SetParent(cardParent);
            pooledCards.Add(newCard);
            cardManager.cardsInDeck.Add(newCard);
        }
        public void RemoveCardFromPool(BaseCard card)
        {
            GameObject cardToRemove = GetCard(card);
            if (cardToRemove != null)
            {
                pooledCards.Remove(cardToRemove);
                cardManager.cardsInDeck.Remove(cardToRemove);
                Destroy(cardToRemove);
            }
        }
    }
}