using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public List<BasePanel> panelList = new();
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void RegisterPanels(BasePanel panel)
    {
        if (!panelList.Contains(panel))
        {
            panelList.Add(panel);
        }
    }
    public BasePanel GetPanel(string panelName)
    {
        foreach (BasePanel panel in panelList)
        {
            if (panel.name == panelName)
            {
                return panel;
            }
        }
        return null;
    }
}
