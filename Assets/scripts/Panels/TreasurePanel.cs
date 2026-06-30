using UnityEngine;
using Item;
using System.Collections.Generic;
using UnityEngine.UI;

public class TreasurePanel : BasePanel
{
    public List<Relic> treasures;

    public GameObject panel;
    public Image treasureImage;
    Relic currentTreasure;

    public void OpenChest()
    {
        if (treasures.Count == 0)
        {
            TakeTreasure();
            return;
        }
        int randomIndex = Random.Range(0, treasures.Count);
        if (treasures[randomIndex] != null)
        {
            treasureImage.sprite = treasures[randomIndex].itemSprite;
            currentTreasure = treasures[randomIndex];
        }
        panel.SetActive(true);
    }
    public void TakeTreasure()
    {
        // Add the treasure to the player's inventory
        ////InventoryManager.instance.AddItem(treasures[randomIndex]);
        currentTreasure.Equip();
        panel.gameObject.SetActive(false);
        ClosePanel();
        GameManager.instance.RoomCleared();
    }
}
