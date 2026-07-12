using System.Collections;
using UnityEngine;

namespace Card
{
    public class CardPack : MonoBehaviour
    {
        public int packCost;
        public int amountCardsContained;
        public CardPackType cardPackType;
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public enum CardPackType
        {
            Attack,
            Defence,
            Ability
        }

        public void Buy()
        {
            bool canBuy = ShopManager.instance.CanBuy(packCost);
            if (canBuy)
            {
                OpenPackAnimation();
                ShopManager.instance.OpenCardPack(amountCardsContained);
                Destroy(gameObject);
            }
        }
        public IEnumerator OpenPackAnimation()
        {
            animator.SetTrigger("Open");
            yield return new WaitForSeconds(2f);
        }
    }
}
