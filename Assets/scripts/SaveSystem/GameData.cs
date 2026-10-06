using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public CharacterData characterData;

    //map
    public int seed;
    public RoomData[] rooms;

    //cards
    public List<CardData> cardsIndeck;
    public List<CardData> cardsInHand;
    public List<CardData> cardsInDiscard;
    public List<CardData> deadCards;

    public GameData()
    {
        seed = 0;
        rooms = null;
        characterData = new CharacterData();
        rooms = new RoomData[35];
        cardsIndeck = new List<CardData>();
        cardsInHand = new List<CardData>();
        cardsInDiscard = new List<CardData>();
        deadCards = new List<CardData>();
    }

    [System.Serializable]
    public class RoomData
    {
        public int x, y = 0;
        public bool isCleared = false;
        public bool isRevealed = false;
        public RoomType roomType = RoomType.Normal;
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
    [System.Serializable]
    public class CardData
    {
        public string cardName;
        public int cardEnergy;
        public string cardDescription;

    }
}

