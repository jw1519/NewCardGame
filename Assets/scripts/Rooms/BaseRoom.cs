using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BaseRoom : MonoBehaviour
{
    public RoomType roomType;
    public Sprite roomImage;
    Image image;
    Button button;
    [HideInInspector] public MapPanel mapPanel;
    public List<Sprite> roomSprites; // List of sprites for different room types, set in the inspector

    public bool isCleared;
    public bool isRevealed; // Whether the room has been revealed on the map

    public int x, y; // Grid coordinates for the room, used in MapPanel when generating the map
    public void Awake()
    {
        isCleared = false;
        isRevealed = false;
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(EnterRoom);
    }
    public void SetUI()
    {
        gameObject.name = roomType.ToString() + " Room (" + x + "," + y + ")";
        switch (roomType)
        {
            case RoomType.Start:
                roomImage = roomSprites[0];
                break;
            case RoomType.Normal:
                roomImage = roomSprites[1];
                break;
            case RoomType.Boss:
                roomImage = roomSprites[2];
                break;
            case RoomType.Shop:
                roomImage = roomSprites[3];
                break;
            case RoomType.Treasure:
                roomImage = roomSprites[4];
                break;
            case RoomType.Secret:
                roomImage = roomSprites[5];
                break;
            case RoomType.End:
                roomImage = roomSprites[6];
                break;
        }
    }
    public void InIt(int  x, int y, RoomType roomType)
    {
        this.x = x;
        this.y = y;
        this.roomType = roomType;
    }
    public void RevealRoom()
    {
        if (isRevealed) return; // If the room is already revealed, do nothing
        isRevealed = true;
        image.sprite = roomImage;
        button.interactable = true; // Enable interaction with the room
    }
    public void ClearRoom()
    {
        isCleared = true;
        GetComponent<Button>().interactable = false; // Disable interaction with the room
        mapPanel.RevealAdjacentRooms(x, y);
        // reveal rooms around this one
    }
    public virtual void EnterRoom()
    {
        switch (roomType)
        {
            case RoomType.Start:
                Debug.Log("Entered Start Room");
                ClearRoom();
                break;
            case RoomType.Normal:
                Debug.Log("Entered Normal Room");
                mapPanel.ClosePanel();
                break;
            case RoomType.Boss:
                Debug.Log("Entered Boss Room");
                mapPanel.ClosePanel();
                break;
            case RoomType.Shop:
                Debug.Log("Entered Shop Room");
                mapPanel.ClosePanel();
                AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("ShopPanel").OpenPanel();
                AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("ShopPanel").GetComponent<ShopPanel>().SetCurrentRoom(this);
                break;
            case RoomType.Treasure:
                Debug.Log("Entered Treasure Room");
                mapPanel.ClosePanel();
                break;
            case RoomType.Secret:
                Debug.Log("Entered Secret Room");
                mapPanel.ClosePanel();
                break;
            case RoomType.End:
                Debug.Log("Entered End Room");
                break;
        }
    }
}
    public enum RoomType
{
    Start,
    Normal,
    Boss,
    Shop,
    Treasure,
    Secret,
    End
}
