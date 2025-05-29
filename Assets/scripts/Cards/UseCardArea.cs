using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class UseCardArea : MonoBehaviour, IDropHandler
    {
        CardHand cardHand;

        private void Awake()
        {
            cardHand = FindAnyObjectByType<CardHand>();
        }
        public virtual void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                if (transform.childCount == 0)
                {
                    eventData.pointerDrag.transform.SetParent(transform, false);
                    cardHand.cards.Remove(eventData.pointerDrag);
                    //eventData.pointerDrag.GetComponent<SetCardUI>().card.Use();
                }
            }
        }
    }
}
