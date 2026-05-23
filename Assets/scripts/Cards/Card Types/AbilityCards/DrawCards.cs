using UnityEngine;
using Character;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability Card/Draw Cards")]
    public class DrawCards : BaseCard
    {
        CardManager cardManager;
        public int cardsToDraw;
        public void Awake()
        {
            cardManager = AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>();
            player = AssetManager.Instance.GetAsset("Player").GetComponent<BaseCharacter>();
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            cardManager.DrawCard(cardsToDraw);
        }
    }
}
