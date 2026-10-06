using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability/Play Random Card")]
    public class PlayRandomCard : BaseCard
    {
        CardManager cardManager;
        public int cardsToDraw;
        public override void Use(GameObject target)
        {
            if (cardManager == null)
            {
                cardManager = AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>();
            }
            base.Use(target);
            cardManager.DrawAndPlayCard();
        }
    }
}
