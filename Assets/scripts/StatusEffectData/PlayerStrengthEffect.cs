using Card;
using UnityEngine;

[CreateAssetMenu(fileName = "StrengthEffect", menuName = "Status Effect/PlayerStrengthEffect")]
public class PlayerStrengthEffect : StatusEffectData
{
    public override void ApplyEffect(GameObject target)
    {
        foreach (GameObject card in CardPool.instance.pooledCards)
        {
            if (card.GetComponent<SetCardUI>().card is AttackCard)
            {
                AttackCard attackCard = card.GetComponent<SetCardUI>().card as AttackCard;
                attackCard.IncreaseDamage(DOTAmount);
                attackCard.UpdateDescritpion();
                card.GetComponent<SetCardUI>().UpdateDescriptionText();
            }
        }
    }
    public override void RemoveEffect()
    {
        foreach (GameObject card in CardPool.instance.pooledCards)
        {
            if (card.GetComponent<SetCardUI>().card is AttackCard)
            {
                AttackCard attackCard = card.GetComponent<SetCardUI>().card as AttackCard;
                attackCard.ResetDamage();
                attackCard.UpdateDescritpion();
                card.GetComponent<SetCardUI>().UpdateDescriptionText();
            }
        }
    }
}
