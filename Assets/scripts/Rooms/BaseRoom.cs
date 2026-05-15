using UnityEngine;

public class BaseRoom : MonoBehaviour
{
    public RoomType roomType;
    public bool isCleared;
    public float roomChance; // Chance for this room to spawn, used in RoomManager when generating rooms
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
