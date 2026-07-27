using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanel : MonoBehaviour
{
    public List<GameObject> panels;

    public void ClosePanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

}
