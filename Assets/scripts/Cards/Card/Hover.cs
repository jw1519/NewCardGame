using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        int index;
        Transform parent;
        BaseCard card;

        SelectManager selectManager;
        private void Start()
        {
            card = GetComponent<SetCardUI>().card;
            selectManager = AssetManager.Instance.GetAsset("SelectManager").GetComponent<SelectManager>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (card.isInHand == false) return;
            if (selectManager.cardSelected == gameObject) return;

            index = transform.GetSiblingIndex();
            parent = transform.parent;
            transform.SetParent(transform.root);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (card.isInHand == false) return;
            if (selectManager.cardSelected == gameObject) return;

            transform.SetParent(parent);
            transform.SetSiblingIndex(index);
        }
    }
}
