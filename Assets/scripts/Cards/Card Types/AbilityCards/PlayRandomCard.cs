using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability/Play Random Card")]
    public class PlayRandomCard : BaseCard
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
            cardManager.DrawAndPlayCard();
        }
    }
}
