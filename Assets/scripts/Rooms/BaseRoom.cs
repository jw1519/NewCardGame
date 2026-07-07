using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BaseRoom : MonoBehaviour
{
    public RoomType roomType;
    public Image image;
    Button button;
    [HideInInspector] public MapPanel mapPanel;

    public bool isCleared;
    public bool isRevealed; // Whether the room has been revealed on the map

    public int x, y; // Grid coordinates for the room, used in MapPanel when generating the map
    public void Awake()
    {
        isCleared = false;
        isRevealed = false;
        button = GetComponent<Button>();
        button.onClick.AddListener(EnterRoom);
        image.gameObject.SetActive(false);
    }
    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void InIt(int  x, int y, RoomType roomType)
    {
        this.x = x;
        this.y = y;
        this.roomType = roomType;
        gameObject.name = roomType.ToString() + " Room (" + x + "," + y + ")";
    }
    public void RevealRoom()
    {
        if (isRevealed) return; // If the room is already revealed, do nothing
        isRevealed = true;
        image.gameObject.SetActive(true); // Show the room's image
        button.interactable = true; // Enable interaction with the room
    }
    public void HideRoom()
    {
        isRevealed = false;
        image.gameObject.SetActive(false); // Hide the room's image
        button.interactable = false; // Disable interaction with the room
    }
    public void ClearRoom()
    {
        isCleared = true;
        GetComponent<Button>().interactable = false; // Disable interaction with the room
        mapPanel.RevealAdjacentRooms(x, y);
    }
    public virtual void EnterRoom()
    {
        switch (roomType)
        {
            case RoomType.Normal:
                GameManager.instance.NewRound();
                mapPanel.ClosePanel();
                GameManager.instance.SetRoom(this);
                break;
            case RoomType.Boss:
                GameManager.instance.NewRound();
                mapPanel.ClosePanel();
                GameManager.instance.SetRoom(this);
                break;
            case RoomType.Shop:
                mapPanel.ClosePanel();
                AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("ShopPanel").OpenPanel();
                GameManager.instance.SetRoom(this);
                break;
            case RoomType.Treasure:
                GameManager.instance.SetRoom(this);
                AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("TreasurePanel").OpenPanel();
                mapPanel.ClosePanel();
                break;
            case RoomType.Secret:
                Debug.Log("Entered Secret Room");
                GameManager.instance.SetRoom(this);
                GameManager.instance.RoomCleared();
                //mapPanel.ClosePanel();
                break;
            case RoomType.End:
                Debug.Log("Entered End Room");
                mapPanel.CreateNewMap();
                break;
        }
    }
}
    public enum RoomType
{
    Normal,
    Boss,
    Shop,
    Treasure,
    Secret,
    End
}
