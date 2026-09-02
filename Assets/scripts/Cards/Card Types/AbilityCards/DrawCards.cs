using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability/Draw Cards")]
    public class DrawCards : BaseCard
    {
        CardManager cardManager;
        public int cardsToDraw;
        public void Awake()
        {
            cardManager = AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>();
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            cardManager.DrawCard(cardsToDraw);
        }
    }
}
