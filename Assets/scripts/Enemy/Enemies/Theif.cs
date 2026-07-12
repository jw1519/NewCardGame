using UnityEngine;
using Card;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Goblin Theif")]
    public class Theif : BaseEnemy
    {
        public override void UseAbility(GameObject target)
        {
            //ability: Steal 1 card from the player
            CardManager cardManager = CardManager.instance;
            if (cardManager.cardsInHand.Count > 0)
            {
                int randomIndex = Random.Range(0, cardManager.cardsInHand.Count);
                GameObject stolenCard = cardManager.cardsInHand[randomIndex];
                stolenCard.GetComponent<UseCard>().DiscardCard();
                Debug.Log("Goblin stole a card from the player!");
            }
            else
            {
                Debug.Log("Goblin tried to steal a card, but the player has no cards in hand.");
            }
        }
    }
}
