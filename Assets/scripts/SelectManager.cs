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
            else
            {
                DeselectCard(card);
            }
        }
        public void DeselectCard(GameObject card)
        {
            Hover hover = cardSelected.gameObject.GetComponent<Hover>();
            hover.Deselect();
            cardSelected = null;
            SelectCard(card);
        }
        public void SelectPlayer(GameObject character)
        {
            if (cardSelected != null)
            {
                cardSelected.card.Use(character);
            }
        }
        public void SelectEnemy(BaseEnemy enemy)
        {
            if (cardSelected != null)
            {
                cardSelected.card.Use(enemy.gameObject);
            }
            else
            {
                Debug.Log("Choose Card to play");
            }
        }
        public void SelectCharacter(BaseCharacter charcter)
        {
            BaseCharacter baseCharacter = characterList.Find(instance => instance.type.ToString() == cardSelected.card.cardType.ToString());
            if (baseCharacter != null)
            {

            }
        }
    }
}
