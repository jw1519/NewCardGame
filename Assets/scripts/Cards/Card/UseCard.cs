using Card;
using Character;
using DG.Tweening;
using UnityEngine;

public class UseCard : MonoBehaviour
{
    BaseCard card;
    BaseCharacter player;
    SelectManager selectManager;
    CardHand cardHand;
    public GameObject discardButton;
    public bool isSelected = false;
    private void Start()
    {
        card = GetComponent<SetCardUI>().card;
        player = AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>().character;
        selectManager = AssetManager.Instance.GetAsset("SelectManager").GetComponent<SelectManager>();
        cardHand = AssetManager.Instance.GetAsset("CardHand").GetComponent<CardHand>();
    }
    private void OnDisable()
    {
        discardButton.SetActive(false);
    }

    public void SelectCard()
    {
        if (card.isInHand == false) return;
        if (isSelected)
        {
            selectManager.DeselectCard();
            return;
        }
        if (selectManager.cardSelected != null)
        {
            selectManager.DeselectCard();
            return;
        }
        selectManager.SelectCard(gameObject);
        cardHand.Hover(gameObject);
        isSelected = true;
        Quaternion rotation = Quaternion.LookRotation(Vector3.zero);
        transform.DORotate(rotation.eulerAngles, 0.1f);
        transform.DOMove(transform.position + 100 * Vector3.up, 0.1f);

        if (discardButton.activeSelf == false)
        {
            discardButton.SetActive(true);
        }
        else
        {
            discardButton.SetActive(false);
        }
    }
    public void DiscardCard()
    {
        if (card.isInHand == false) return;
        if (isSelected)
        {
            selectManager.DeselectCard();
        }
        AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().DiscardCard(gameObject);
        cardHand.StartCoroutine(cardHand.RemoveCard(gameObject));
        card.isInHand = false;
    }
}
