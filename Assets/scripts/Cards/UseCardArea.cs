using Character;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class UseCardArea : MonoBehaviour, IDropHandler
    {
        CardHand cardHand;
        BaseCharacter player;

        private void Awake()
        {
            cardHand = FindAnyObjectByType<CardHand>();
            player = FindFirstObjectByType<BaseCharacter>();
        }
        public virtual void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                if (player.energy - eventData.pointerDrag.gameObject.GetComponent<SetCardUI>().card.cardEnergy >= 0)
                {
                    if (transform.childCount == 0)
                    {
                    eventData.pointerDrag.transform.SetParent(transform, false);
                    cardHand.cards.Remove(eventData.pointerDrag);
                    CardManager.instance.cardsInHand.Remove(eventData.pointerDrag);
                    SelectManager.instance.SelectCard(eventData.pointerDrag);
                    }
                }
                else
                {
                    Debug.Log("not enough energy");
                }

            }
        }
    }
}
