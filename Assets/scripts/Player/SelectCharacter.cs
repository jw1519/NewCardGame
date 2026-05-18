using Card;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public void OnClick()
    {
        AssetManager.Instance.GetAsset("SelectManager").GetComponent<SelectManager>().UseCard(gameObject);
    }
}
