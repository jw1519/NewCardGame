using UnityEngine;

public class MapPanel : BasePanel
{
    public Transform roomContainer;
    public GameObject roomPrefab;

    public int mapSize = 5; // Size of the map (5x5)
    public float roomSpacing = 1.5f; // Spacing between rooms

    BaseRoom[,] grid;

    public void CreateMap()
    {

    }
}
