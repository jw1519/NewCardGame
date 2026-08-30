using UnityEngine;
using Item;

public class TreasureRoom : BaseRoom
{
    public Relic treasure;
    TreasurePanel treasurePanel;

    private void Start()
    {
        treasurePanel = AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("TreasurePanel").GetComponent<TreasurePanel>();
    }
    public void RoomSetUp(Relic item)
    {
        treasure = Instantiate(item);
    }
    public override void EnterRoom()
    {
        GameManager.instance.SetRoom(this);
        treasurePanel.SetTreasure(treasure);
        treasurePanel.OpenPanel();
        mapPanel.ClosePanel();
    }
}
