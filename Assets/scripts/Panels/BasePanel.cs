using UnityEngine;

public class BasePanel : MonoBehaviour
{
    private void Start()
    {
        if (UIManager.instance != null)
            UIManager.instance.RegisterPanels(this);
    }
    public virtual void OpenPanel()
    {
        gameObject.SetActive(true);
    }
    public virtual void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
