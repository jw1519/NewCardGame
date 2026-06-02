using UnityEngine;

public class MapPanel : BasePanel
{
    public Transform roomContainer;
    public GameObject roomPrefab;

    public int mapSize = 5; // Size of the map (5x5)
    public float roomSpacing = 1.5f; // Spacing between rooms
    public float roomSize = 1f; // Size of each room

    BaseRoom[,] grid;
    public void Start()
    {
        grid = new BaseRoom[mapSize, mapSize];
        CreateMap();
    }

    public void CreateMap()
    {
        for (int  i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                GameObject room = Instantiate(roomPrefab, new Vector3( i * roomSize - 5 * roomSpacing, j * roomSize - 5 * roomSpacing, 0), Quaternion.identity);
                room.transform.localScale = Vector3.one * roomSize;
                room.transform.SetParent(roomContainer, false);

                grid[i, j] = room.GetComponent<BaseRoom>();

                int roomTypeRoll = Random.Range(0, 100);

                switch (roomTypeRoll)
                {
                    case int n when (n < 60):
                        grid[i, j].InIt(i, j, RoomType.Normal);
                        break;
                    case int n when (n < 80):
                        grid[i, j].InIt(i, j, RoomType.Boss);
                        break;
                    case int n when (n < 90):
                        grid[i, j].InIt(i, j, RoomType.Shop);
                        break;
                    case int n when (n < 95):
                        grid[i, j].InIt(i, j, RoomType.Treasure);
                        break;
                    default:
                        grid[i, j].InIt(i, j, RoomType.Secret);
                        break;
                }

                if (i == 0 && j == 0)
                {
                    grid[i, j].InIt(i, j, RoomType.Start);
                    room.GetComponent<BaseRoom>().RevealRoom(); // Reveal the starting room
                }
                else if (i == mapSize - 1 && j == mapSize - 1)
                {
                    grid[i, j].InIt(i, j, RoomType.End);
                }
                room.GetComponent<BaseRoom>().SetUI();

            }
        }
    }
}
