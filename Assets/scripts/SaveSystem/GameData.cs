using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public class GameData
{
    public CharacterData characterData;
    public List<string> items;
    public List<string> relics;

    //map
    public int seed;
    public RoomData[] rooms;

    //cards
    public List<string> cardsInDeck;
    public List<string> cardsInHand;
    public List<string> cardsInDiscard;
    public List<string> deadCards;

    //collection panel
    public List<string> enemiesFound;

    public GameData()
    {
        seed = 0;
        rooms = null;
        characterData = new CharacterData();
        rooms = new RoomData[35];
        cardsInDeck = new();
        cardsInHand = new();
        cardsInDiscard = new();
        deadCards = new();
        items = new();
        relics = new();
    }

    [System.Serializable]
    public class RoomData
    {
        public int x, y = 0;
        public bool isCleared = false;
        public bool isRevealed = false;
        public RoomType roomType = RoomType.Normal;
        public string enemyName;
        public int enemyAmount;
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
        public int maxItemAmount = 3;
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

