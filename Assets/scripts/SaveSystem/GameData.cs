using Character;

public class GameData
{
    public BaseCharacter baseCharacter;

    //map
    public int seed;
    public RoomData[] rooms;

    [System.Serializable]
    public class RoomData
    {
        public int x, y = 0;
        public bool isCleared = false;
        public bool isRevealed = false;
    }

    public GameData()
    {
        baseCharacter = null;
        seed = 0;
        rooms = null;
    }
}

