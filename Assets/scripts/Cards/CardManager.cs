using Character;
using Enemy;
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

        BaseCharacter player;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            hand = FindAnyObjectByType<CardHand>();
        }
        public void Start()
        {
            player = AssetManager.Instance.GetAsset("Player").GetComponent<BaseCharacter>();
        }
        public void NewRound()
        {
            EmptyDiscardPile();
            if (cardsInDeck.Count >= startingCardsInHand)
            {
                DrawCard(startingCardsInHand);
            }
            else
            {
                EmptyDiscardPile();
                NewRound();
            }
        }
        public void DrawCard(int amount)
        {
            AssetManager.Instance.GetAsset("SelectManager").GetComponent<SelectManager>().DeselectCard();
            if (cardsInHand.Count >= maxCardsInHand)
            {
                Debug.Log("hand is full");
                return;
            }
            if (cardsInDeck.Count > 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    if (cardsInHand.Count == maxCardsInHand)
                    {
                        Debug.Log("hand is full");
                        return;
                    }
                    GameObject RandomCard = CardPool.instance.GetPooledCard();
                    if (RandomCard != null)
                    {
                        RandomCard.GetComponent<SetCardUI>().card.isInHand = true;
                        RandomCard.gameObject.SetActive(true);
                        RandomCard.transform.SetParent(hand.transform, false);
                        cardsInHand.Add(RandomCard);
                        StartCoroutine(hand.AddCard(RandomCard));
                        cardsInDeck.Remove(RandomCard);
                    }
                }

            }
            else
            {
                EmptyDiscardPile();
                DrawCard(amount);
            }
        }
        
        public void DrawAndPlayCard()
        {
            if (cardsInDeck.Count > 0)
            {
                GameObject RandomCard = CardPool.instance.GetPooledCard();
                if (RandomCard != null)
                {
                    RandomCard.transform.SetParent(discardedCardParent.transform, false);
                    cardsInDeck.Remove(RandomCard);
                    cardsInDiscard.Add(RandomCard);
                    
                    BaseCard card = RandomCard.GetComponent<SetCardUI>().card;

                    CombatManager combatManager = AssetManager.Instance.GetAsset("CombatManager").GetComponent<CombatManager>();
                    player.GainEnergy(card.cardEnergy);
                    switch (card.cardType)
                    {
                        case BaseCard.CardType.Attack:
                            card.Use(combatManager.combatOrder.Find(p => p.GetComponent<SetEnemyUI>() != null));
                            break;
                        case BaseCard.CardType.Defence:
                            card.Use(combatManager.combatOrder.Find(p => p.GetComponent<BaseCharacter>() != null));
                            break;
                        case BaseCard.CardType.Ability:
                            card.Use(combatManager.combatOrder.Find(p => p.GetComponent<BaseCharacter>() != null));
                            break;
                    }
                }
            }
            else
            {
                EmptyDiscardPile();
                DrawAndPlayCard();
            }
        }
        public void EmptyDiscardPile()
        {
            // put cards from discard pile into deck if deck is empty or doesnt have enough cards
            foreach (GameObject card in cardsInDiscard)
            {
                cardsInDeck.Add(card);
                card.transform.SetParent(deckCardParent, false);
            }
            cardsInDiscard.Clear();
        }
        //discard any unused cards when turn ends
        public void DiscardAllCards()
        {
            // add all cards in hand to discard list
            foreach (GameObject card in cardsInHand)
            {
                card.GetComponent<SetCardUI>().card.isInHand = false;
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
            cardsInDiscard.Add(card);
        }
        public void AddCard(GameObject card)
        {
            cardsInDeck.Add(card);
            card.transform.SetParent(deckCardParent);
        }
    }
}
