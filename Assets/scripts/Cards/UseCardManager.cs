using UnityEngine;
using Enemy;

namespace Card
{
    public class SelectManager : MonoBehaviour
    {
        public static SelectManager instance;
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
                Debug.Log("Attack enemy");
            }
            else
            {
                Debug.Log("Choose Card to play");
            }
        }
    }
}
