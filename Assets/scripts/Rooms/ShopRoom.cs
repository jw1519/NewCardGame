using UnityEngine;

public class ShopRoom : BaseRoom
{
    ShopPanel shopPanel;
    void Start()
    {
        shopPanel = AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("ShopPanel").GetComponent<ShopPanel>();
    }

    public override void EnterRoom()
    {
        shopPanel.SetCurrentRoom(this);
        shopPanel.OpenPanel();
    }
}
