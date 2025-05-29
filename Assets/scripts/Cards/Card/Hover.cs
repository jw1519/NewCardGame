using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Card
{
    public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        int index;
        Transform parent;

        public void OnPointerEnter(PointerEventData eventData)
        {
            index = transform.GetSiblingIndex();
            parent = transform.parent;
            transform.SetParent(transform.root);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.SetParent(parent);
            transform.SetSiblingIndex(index);
        }
        public void OnClick()
        {
            if (enabled)
            {
                index = transform.GetSiblingIndex();
                parent = transform.parent;
                transform.SetParent(transform.root);
                SelectManager.instance.SelectCard(gameObject);
                enabled = false;
            }
            else
            {
                Deselect();
                SelectManager.instance.cardSelected = null;
            }
        }
        public void Deselect()
        {
            transform.SetParent(parent);
            transform.SetSiblingIndex(index);
            enabled = true;
        }
    }
}
