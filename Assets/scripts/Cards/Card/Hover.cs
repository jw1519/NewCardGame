using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        int index;
        BaseCard card;

        SelectManager selectManager;
        [HideInInspector] public CardHand hand;
        private void Start()
        {
            card = GetComponent<SetCardUI>().card;
            selectManager = AssetManager.Instance.GetAsset("SelectManager").GetComponent<SelectManager>();
            hand = AssetManager.Instance.GetAsset("CardHand").GetComponent<CardHand>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (card.isInHand == false) return;
            if (selectManager.cardSelected == gameObject) return;
            if (GetComponent<UseCard>().isSelected == true) return;
            hand.Hover(gameObject);
            HoverCard();
        }
        public void HoverCard()
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.zero);
            transform.DORotate(rotation.eulerAngles, 0.1f);
            transform.DOMove(transform.position + 100 * Vector3.up, 0.1f);
        }
        public void ResetCard()
        {
            transform.SetSiblingIndex(index);
        }
        public void SetIndex(int newIndex)
        {
            index = newIndex;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (card.isInHand == false) return;
            if (selectManager.cardSelected == gameObject) return;

            if (GetComponent<UseCard>().isSelected == false)
            {
                transform.SetSiblingIndex(index);
                hand.StartCoroutine(hand.UpdateCardPositions(0.1f));
            }
        }
    }
}
