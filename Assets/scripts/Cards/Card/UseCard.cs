using Card;
using Character;
using UnityEngine;

public class UseCard : MonoBehaviour
{
    BaseCard card;
    BaseCharacter player;
    public Vector3 pos;
    public Transform parent;
    SelectManager selectManager;
    CardHand cardHand;
    public GameObject discardButton;
    bool isSelected = false;
    private void Start()
    {
        card = GetComponent<SetCardUI>().card;
        player = FindAnyObjectByType<BaseCharacter>();
        selectManager = AssetManager.Instance.GetAsset("SelectManager").GetComponent<SelectManager>();
        cardHand = AssetManager.Instance.GetAsset("CardHand").GetComponent<CardHand>();
    }
    private void OnEnable()
    {
        parent = transform.parent;
        pos = transform.localPosition;
    }
    private void OnDisable()
    {
        parent = null;
        discardButton.SetActive(false);
    }

    public void SelectCard()
    {
        if (card.isInHand == false) return;
        if (isSelected)
        {
            DeselectCard();
            return;
        }
        if (selectManager.cardSelected != null)
        {
            DeselectCard();
            return;
        }
        if (player.energy - card.cardEnergy >= 0)
        {
            selectManager.SelectCard(gameObject);
            pos = transform.localPosition;
            transform.localPosition = new Vector3(pos.x, pos.y + 100f, pos.z);
            transform.SetParent(transform.parent.root);
            isSelected = true;
        }
        else
        {
            Debug.Log("not enough energy");
        }
        if (discardButton.activeSelf == false)
        {
            discardButton.SetActive(true);
        }
        else
        {
            discardButton.SetActive(false);
        }
    }
    public void DeselectCard()
    {
        if (card.isInHand == false) return;
        if (!isSelected) return;
        selectManager.cardSelected = null;
        isSelected = false;
        transform.SetParent(parent);
        discardButton.SetActive(false);
        discardButton.transform.SetParent(transform);
        cardHand.StartCoroutine(cardHand.UpdateCardPositions(0));
    }
    public void DiscardCard()
    {
        if (card.isInHand == false) return;
        DeselectCard();
        cardHand.cards.Remove(gameObject);
        cardHand.StartCoroutine(cardHand.UpdateCardPositions(0.15f));
        AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().DiscardCard(gameObject);
    }
}
