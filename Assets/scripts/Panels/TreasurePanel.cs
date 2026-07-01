using UnityEngine;
using Item;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Character;

public class TreasurePanel : BasePanel
{
    public List<Relic> treasures;

    public List<Relic> instantiatedTreasures;

    public GameObject panel;
    public Image treasureImage;
    Relic currentTreasure;

    public TextMeshProUGUI treasureDescrition;

    private void Start()
    {
        for (int i = 0; i < treasures.Count; i++)
        {
            instantiatedTreasures.Add(Instantiate(treasures[i]));
        }
    }

    public void OpenChest()
    {
        if (instantiatedTreasures.Count == 0)
        {
            TakeTreasure();
            return;
        }
        int randomIndex = Random.Range(0, instantiatedTreasures.Count);
        if (treasures[randomIndex] != null)
        {
            treasureImage.sprite = instantiatedTreasures[randomIndex].itemSprite;
            currentTreasure = instantiatedTreasures[randomIndex];
        }
        panel.SetActive(true);
        treasureDescrition.text = currentTreasure.description;
    }
    public void TakeTreasure()
    {
        // Add the treasure to the player's inventory

        PlayerStatsPanel panel = AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("PlayerStatsPanel").GetComponent<PlayerStatsPanel>();

        if (panel != null && panel.CanAddRelic())
        {
            panel.AddRelic(currentTreasure);
            ClosePanel();
        }
        else
        {
            Debug.Log("Cannot add more relics. Inventory is full.");
        }
    }
    public override void ClosePanel()
    {
        base.ClosePanel();
        GameManager.instance.RoomCleared();
        panel.SetActive(false);
    }
}
