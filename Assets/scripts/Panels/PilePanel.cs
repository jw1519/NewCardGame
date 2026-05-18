using UnityEngine;

public class PilePanels : BasePanel
{
    public Transform content;
    public override void OpenPanel()
    {
        base.OpenPanel();
        UpdateCards();
    }
    public void UpdateCards()
    {
        foreach (Transform child in content)
        {
            child.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
