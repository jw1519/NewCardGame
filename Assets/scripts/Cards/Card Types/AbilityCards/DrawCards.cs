using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability/Draw Cards")]
    public class DrawCards : BaseCard
    {
        CardManager cardManager;
        public int cardsToDraw;
        public override void Use(GameObject target)
        {
            if (cardManager == null)
                cardManager = AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>();
            base.Use(target);
            cardManager.DrawCard(cardsToDraw);
            Debug.Log("Drew " + cardsToDraw + " cards");
        }
    }
}
