using Card;
using Character;
using UnityEngine;
using UnityEngine.UIElements;

public class UseCard : MonoBehaviour
{
    BaseCard card;
    BaseCharacter player;
    public Vector3 pos;
    Transform parent;
    bool isSelected = false;
    private void Start()
    {
        card = GetComponent<SetCardUI>().card;
        player = FindAnyObjectByType<BaseCharacter>();
    }
    private void OnEnable()
    {
        parent = transform.GetComponentInParent<Transform>();
    }

    public void SelectCard()
    {
        if (card.isInHand == false) return;
        if (isSelected)
        {
            DeselectCard();
            return;
        }
        if (SelectManager.instance.cardSelected != null)
        {
            DeselectCard();
            return;
        }
        if (player.energy - card.cardEnergy >= 0)
        {
            SelectManager.instance.SelectCard(gameObject);
            pos = transform.position;
            transform.position = new Vector3(pos.x, pos.y + 100f, pos.z);
            transform.SetParent(transform.root);
            isSelected = true;
        }
        else
        {
            Debug.Log("not enough energy");
        }
    }
    public void DeselectCard()
    {
        if (card.isInHand == false) return;
        if (!isSelected) return;
        SelectManager.instance.cardSelected = null;
        isSelected = false;
        transform.position = pos;
        transform.SetParent(parent);
    }
}
