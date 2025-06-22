using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardSelect : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(OnSelect); 
        }
        public void OnSelect()
        {
            CardManager.instance.AddCard(gameObject);
            ShopManager.instance.CardSelected();
            Destroy(gameObject.GetComponent<CardSelect>());
        }
    }
}
