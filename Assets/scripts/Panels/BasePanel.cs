using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    private void Start()
    {
        UIManager.instance.RegisterPanels(this);
    }
    public virtual void OpenPanel()
    {
        gameObject.SetActive(true);
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
