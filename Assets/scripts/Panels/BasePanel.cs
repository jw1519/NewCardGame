using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    private void Awake()
    {
        UIManager.instance.RegisterPanels(this);
    }
    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
