using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseRoom : MonoBehaviour
{
    public RoomType roomType;
    public Sprite roomImage;
    Image image;
    public TextMeshProUGUI text;

    public bool isCleared;
    public float roomChance; // Chance for this room to spawn, used in RoomManager when generating rooms
    public bool isRevealed; // Whether the room has been revealed on the map

    public int x, y; // Grid coordinates for the room, used in MapPanel when generating the map
    public void Awake()
    {
        isCleared = false;
        isRevealed = false;
        image = GetComponent<Image>();
        text.text = roomType.ToString();
        GetComponent<Button>().onClick.AddListener(EnterRoom);
        if (roomType != RoomType.Start)
            GetComponent<Button>().interactable = false; // Disable interaction until the room is revealed
    }
    public void SetUI()
    {
        text.text = roomType.ToString();
        gameObject.name = roomType.ToString() + " Room (" + x + "," + y + ")";
    }
    public void InIt(int  x, int y, RoomType roomType)
    {
        this.x = x;
        this.y = y;
        this.roomType = roomType;
    }
    public void RevealRoom()
    {
        isRevealed = true;
        image.sprite = roomImage;
    }
    public void ClearRoom()
    {
        isCleared = true;
        GetComponent<Button>().interactable = false; // Disable interaction with the room
        // reveal rooms around this one
    }
    public virtual void EnterRoom()
    {
        switch (roomType)
        {
            case RoomType.Normal:
                Debug.Log("Entered Normal Room");
                break;
            case RoomType.Boss:
                Debug.Log("Entered Boss Room");
                break;
            case RoomType.Shop:
                Debug.Log("Entered Shop Room");
                break;
            case RoomType.Treasure:
                Debug.Log("Entered Treasure Room");
                break;
            case RoomType.Secret:
                Debug.Log("Entered Secret Room");
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
