using UnityEngine;

public class UseCard : MonoBehaviour
{
    DragAndDrop dragAndDrop;
    void Awake()
    {
        dragAndDrop = GetComponent<DragAndDrop>();
    }
}
