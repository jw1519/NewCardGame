using UnityEngine;
using UnityEngine.UI;

public class BaseRoom : MonoBehaviour
{
    public RoomType roomType;
    Image image;
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
        image = transform.GetChild(0).GetComponent<Image>();
        image.gameObject.SetActive(false);
    }

    public virtual void RoomSetUp()
    {

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
            case RoomType.Shop:
                mapPanel.ClosePanel();
                AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("ShopPanel").OpenPanel();
                GameManager.instance.SetRoom(this);
                break;
            case RoomType.healOrUpgrade:
                Debug.Log("Entered Campfire Room");
                GameManager.instance.SetRoom(this);
                AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("HealOrUpgradePanel").OpenPanel();
                mapPanel.ClosePanel();
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
    healOrUpgrade,
    End
}
