using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using Unity.VisualScripting;
using Enemy;

namespace Card
{
    public class UseCardManager : MonoBehaviour
    {
        public static UseCardManager instance;
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
        public void SelectEnemy(BaseEnemy enemy)
        {
            if (cardSelected != null)
            {

            }
        }
    }
}
