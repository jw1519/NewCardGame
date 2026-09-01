using Card;
using Character;
using DG.Tweening;
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
    [HideInInspector] public bool isSelected = false;
    private void Start()
    {
        card = GetComponent<SetCardUI>().card;
        player = AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>().character;
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
            //pos = transform.localPosition;

            transform.SetParent(transform.parent.root);
            isSelected = true;
            Quaternion rotation = Quaternion.LookRotation(Vector3.zero);
            transform.DORotate(rotation.eulerAngles, 0.1f);
            transform.DOMove(transform.position + 100 * Vector3.up, 0.1f);

            //transform.localPosition = new Vector3(pos.x, pos.y + 100f, pos.z);
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
        //if (!isSelected) return;
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
        card.isInHand = false;
        cardHand.StartCoroutine(cardHand.UpdateCardPositions(0.15f));
        AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().DiscardCard(gameObject);
    }
}
