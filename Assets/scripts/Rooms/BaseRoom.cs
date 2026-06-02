using UnityEngine;
using UnityEngine.UI;

public class BaseRoom : MonoBehaviour
{
    public RoomType roomType;
    public Sprite roomImage;

    public bool isCleared;
    public float roomChance; // Chance for this room to spawn, used in RoomManager when generating rooms
    public bool isRevealed; // Whether the room has been revealed on the map

    public int x, y; // Grid coordinates for the room, used in MapPanel when generating the map
    public void Awake()
    {
        isCleared = false;
        isRevealed = false;
        GetComponent<Image>().sprite = roomImage;
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
