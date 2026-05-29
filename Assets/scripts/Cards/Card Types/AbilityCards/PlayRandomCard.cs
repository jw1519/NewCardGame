using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability Card/Play Random Card")]
    public class PlayRandomCard : BaseCard
    {
        CardManager cardManager;
        public int cardsToDraw;
        public override void Awake()
        {
            base.Awake();
            cardManager = AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>();
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            cardManager.DrawAndPlayCard();
        }
    }
}
