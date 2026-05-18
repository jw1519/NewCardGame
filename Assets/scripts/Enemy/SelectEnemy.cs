using Card;
using UnityEngine;

namespace Enemy
{
    public class SelectEnemy : MonoBehaviour
    {
        public void OnClick()
        {
            AssetManager.Instance.GetAsset("SelectManager").GetComponent<SelectManager>().UseCard(gameObject);
        }
    }
}
