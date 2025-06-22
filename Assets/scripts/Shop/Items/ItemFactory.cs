using UnityEngine;

namespace Item
{
    public class ItemFactory : MonoBehaviour
    {
        public static ItemFactory instance;
        public GameObject itemPrefab;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            itemPrefab.SetActive(false);
        }
        public GameObject CreateItem(BaseItem item)
        {
            GameObject instance = Instantiate(itemPrefab);
            instance.GetComponent<SetItemUI>().item = item;
            instance.SetActive(true);
            return instance;
        }
    }
}