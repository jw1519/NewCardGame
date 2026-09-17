using UnityEngine;
using Card;

[CreateAssetMenu(fileName = "WeaknessEffect", menuName = "Status Effect/PlayerWeaknessEffect")]
public class WeaknessEffect : StatusEffectData
{
    public override void ApplyEffect(GameObject target)
    {
        foreach (GameObject card in CardPool.instance.pooledCards)
        {
            if (card.GetComponent<SetCardUI>().card is AttackCard)
            {
                AttackCard attackCard = card.GetComponent<SetCardUI>().card as AttackCard;
                attackCard.DecreaseDamage(DOTAmount);
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
