using System.Collections.Generic;
using UnityEngine;

public class CollectionPanel : BasePanel
{
    public List<GameObject> panels;


    private void Start()
    {
    }
    public void ClosePanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
    public void OpenEnemyPanel()
    {
        ClosePanels();
        foreach (GameObject panel in panels)
        {
            if (panel.name == "EnemiesPanel")
            {
                panel.SetActive(true);
            }
        }
    }

}
