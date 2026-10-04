using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public CharacterData characterData;

    //map
    public int seed;
    public RoomData[] rooms;

    public GameData()
    {
        seed = 0;
        rooms = null;
        characterData = new CharacterData();
        rooms = new RoomData[35];
    }

    [System.Serializable]
    public class RoomData
    {
        public int x, y = 0;
        public bool isCleared = false;
        public bool isRevealed = false;
    }
    [System.Serializable]
    public class CharacterData
    {
        public string characterName = null;
        public int health = 20;
        public int maxHealth = 20;
        public int defence = 0;
        public int energy = 3;
        public int maxEnergy = 3;
        public int gold = 0;
        public int totalGoldCollected = 0;
        public List<EffectData> activeEffects = new();
    }
    [System.Serializable]
    public class EffectData
    {
        public string effectName;
        public float DOTAmount;
        public int duration;
        public bool doesDamage;
        public string description;
    }
}

