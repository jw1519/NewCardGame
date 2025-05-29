using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform parentAfterDrag;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    CardHand cardHand;

    public bool isDragging;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        cardHand = FindAnyObjectByType<CardHand>();
        isDragging = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (transform.parent == transform.root)
        {
            transform.SetParent(parentAfterDrag, false);
        }
        parentAfterDrag = transform.parent;
        StartCoroutine(cardHand.UpdateCardPositions(0.15f));
        
        isDragging = false;
    }
}