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
            pooledCards = new List<GameObject>();
            GameObject gameObject;
            foreach (BaseCard card in cardSO)
            {
                gameObject = Instantiate(cardToPool);
                gameObject.GetComponent<SetCardUI>().card = Instantiate(card);
                gameObject.transform.SetParent(cardParent);
                pooledCards.Add(gameObject);
            }
            CardManager.instance.cardsInDeck = pooledCards;
        }
        public GameObject GetPooledCard()
        {
            List<GameObject> cardDeck = CardManager.instance.cardsInDeck;
            GameObject randomCard = cardDeck[Random.Range(0, cardDeck.Count)];
            return randomCard;
        }
    }
}