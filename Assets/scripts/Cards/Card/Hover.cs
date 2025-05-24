using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Card
{
    public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        int index;
        Transform parent;
        public Outline outline;

        public void OnPointerEnter(PointerEventData eventData)
        {
            outline.enabled = true;
            index = transform.GetSiblingIndex();
            parent = transform.parent;
            transform.SetParent(transform.root);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            outline.enabled = false;
            transform.SetParent(parent);
            transform.SetSiblingIndex(index);
        }
        public void OnClick()
        {
            if (enabled)
            {
                outline.enabled = true;
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
            outline.enabled = false;
            transform.SetParent(parent);
            transform.SetSiblingIndex(index);
            enabled = true;
        }
    }
}
