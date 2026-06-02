using UnityEngine;

public class MapPanel : BasePanel
{
    public Transform roomContainer;
    public GameObject roomPrefab;

    public int mapSize = 5; // Size of the map (5x5)
    public float roomSize = 1f; // Size of each room

    BaseRoom[,] grid;
    public void Start()
    {
        grid = new BaseRoom[mapSize, mapSize];
        CreateMap();
    }

    public void CreateMap()
    {
        for (int  x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                GameObject room = Instantiate(roomPrefab, new Vector3(x * roomSize -200, y * roomSize - 200, 0), Quaternion.identity);
                room.transform.localScale = Vector3.one * roomSize;
                room.transform.SetParent(roomContainer, false);

                grid[x, y] = room.GetComponent<BaseRoom>();

                int roomTypeRoll = Random.Range(0, 100);

                switch (roomTypeRoll)
                {
                    case int n when (n < 60):
                        grid[x, y].InIt(x, y, RoomType.Normal);
                        break;
                    case int n when (n < 80):
                        grid[x, y].InIt(x, y, RoomType.Boss);
                        break;
                    case int n when (n < 90):
                        grid[x, y].InIt(x, y, RoomType.Shop);
                        break;
                    case int n when (n < 95):
                        grid[x, y].InIt(x, y, RoomType.Treasure);
                        break;
                    default:
                        grid[x, y].InIt(x, y, RoomType.Secret);
                        break;
                }

                if (x == 0 && y == 0)
                {
                    grid[x, y].InIt(x, y, RoomType.Start);
                    room.GetComponent<BaseRoom>().RevealRoom(); // Reveal the starting room
                }
                else if (x == mapSize - 1 && y == mapSize - 1)
                {
                    grid[x, y].InIt(x, y, RoomType.End);
                }
                grid[x, y].SetUI();
            }
        }
    }
}
