using UnityEditor;
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
    }
}
