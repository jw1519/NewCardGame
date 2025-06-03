using UnityEngine;
using Enemy;
using System.Collections.Generic;
using Character;

namespace Card
{
    public class SelectManager : MonoBehaviour
    {
        public static SelectManager instance;
        public List<BaseCharacter> characterList;
        [HideInInspector] public SetCardUI cardSelected;

        private void Awake()
        {
            instance = this;
        }
        public void SelectCard(GameObject card)
        {
            if (cardSelected == null)
            {
                cardSelected = card.GetComponent<SetCardUI>();
            }
        }
        public void SelectPlayer(GameObject character)
        {
            if (cardSelected != null)
            {
                if (cardSelected.card.cardType == BaseCard.CardType.Defence)
                {
                    cardSelected.card.Use(character);
                    CardManager.instance.DiscardCard(cardSelected.gameObject);
                    cardSelected = null;
                }
            }
        }
        public void SelectEnemy(BaseEnemy enemy)
        {
            if (cardSelected != null)
            {
                if (cardSelected.card.cardType == BaseCard.CardType.Attack)
                {
                    cardSelected.card.Use(enemy.gameObject);
                    CardManager.instance.DiscardCard(cardSelected.gameObject);
                    cardSelected = null;
                }
                else
                {
                    Debug.Log("Select Character");
                }
            }
        }
    }
}
