using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        int index;
        Transform parent;
        BaseCard card;
        private void Start()
        {
            card = GetComponent<SetCardUI>().card;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (card.isInHand == false) return;

            index = transform.GetSiblingIndex();
            parent = transform.parent;
            transform.SetParent(transform.root);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (card.isInHand == false) return;

            transform.SetParent(parent);
            transform.SetSiblingIndex(index);
        }
    }
}
