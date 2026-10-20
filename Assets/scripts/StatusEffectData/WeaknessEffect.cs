using UnityEngine;
using Card;
using Enemy;
using Character;

[CreateAssetMenu(fileName = "WeaknessEffect", menuName = "Status Effect/PlayerWeaknessEffect")]
public class WeaknessEffect : StatusEffectData
{
    public override void ApplyEffect(GameObject target)
    {
        if (target.GetComponent<SetCharacterUI>())
        {
            applyOnEnemy = false;
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
        else
        {
            applyOnEnemy = true;
            target.GetComponent<SetEnemyUI>().enemy.DecreaseDamage(DOTAmount);
            target.GetComponent<SetEnemyUI>().UpdateActionUI();
        }
    }
    public override void RemoveEffect()
    {
        if (!applyOnEnemy)
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
}
