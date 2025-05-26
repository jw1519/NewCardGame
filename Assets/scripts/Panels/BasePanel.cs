using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
