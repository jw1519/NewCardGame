using System;
using System.Collections.Generic;
using UnityEngine;

public class MapPanel : BasePanel
{
    

    public Transform roomContainer;
    public GameObject roomPrefab;
    public List<BaseRoom> rooms; // List to hold references to all rooms, set in the inspector

    public int mapWidth = 5; // Size of the map (5x5)
    public int mapHeight = 5; // Height of the map (5x5)
    public float roomSize = 1f; // Size of each room

    public List<Sprite> roomSprites; // List of sprites for different room types, set in the inspector

    BaseRoom[,] grid;
    public void Start()
    {
        grid = new BaseRoom[mapWidth, mapHeight];
        Events.roomCleared += ClearRoom; // Subscribe to the roomCleared event
        CreateMap();
    }
    private void OnDestroy()
    {
        Events.roomCleared -= ClearRoom;
    }
    public void CreateNewMap()
    {
        foreach (Transform child in roomContainer)
        {
            Destroy(child.gameObject);
        }
        grid = new BaseRoom[mapWidth, mapHeight];
        CreateMap();
    }

    public void CreateMap()
    {
        for (int  x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject room = Instantiate(roomPrefab, new Vector3(x * roomSize - roomSize * 2, y * roomSize - roomSize * 3, 0), Quaternion.identity);
                room.transform.localScale = Vector3.one * roomSize;
                room.transform.SetParent(roomContainer, false);
                room.GetComponent<BaseRoom>().mapPanel = this; // Set reference to MapPanel in each room

                grid[x, y] = room.GetComponent<BaseRoom>();

                int roomTypeRoll = UnityEngine.Random.Range(0, 100);

                switch (roomTypeRoll)
                {
                    case int n when (n < 60):
                        grid[x, y].InIt(x, y, RoomType.Normal);
                        grid[x, y].SetSprite(roomSprites[0]);
                        break;
                    case int n when (n < 80):
                        grid[x, y].InIt(x, y, RoomType.Boss);
                        grid[x, y].SetSprite(roomSprites[1]);
                        break;
                    case int n when (n < 90):
                        grid[x, y].InIt(x, y, RoomType.Shop);
                        grid[x, y].SetSprite(roomSprites[2]);
                        break;
                    case int n when (n < 95):
                        grid[x, y].InIt(x, y, RoomType.Treasure);
                        grid[x, y].SetSprite(roomSprites[3]);
                        break;
                    default:
                        grid[x, y].InIt(x, y, RoomType.Secret);
                        grid[x, y].SetSprite(roomSprites[4]);
                        break;
                }

                if (y == 0)
                {
                    room.GetComponent<BaseRoom>().RevealRoom(); // Reveal the starting room
                }
                else if (x == mapWidth - 1 && y == mapWidth - 1)
                {

                    grid[x, y].InIt(x, y, RoomType.End);
                    grid[x, y].SetSprite(roomSprites[5]);
                }
            }
        }
    }

    public void RevealAdjacentRooms(int x, int y)
    {
        // Reveal adjacent rooms (up, down, left, right)
        if (x > 0) grid[x - 1, y].RevealRoom();
        if (x < mapWidth - 1) grid[x + 1, y].RevealRoom();
        if (y > 0) grid[x, y - 1].RevealRoom();
        if (y < mapHeight - 1) grid[x, y + 1].RevealRoom();
    }
    public void ClearRoom(int x, int y)
    {
        grid[x, y].ClearRoom();
        OpenPanel();
    }
}
