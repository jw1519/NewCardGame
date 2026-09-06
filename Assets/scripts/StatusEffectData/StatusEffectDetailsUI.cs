using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectDetailsUI : MonoBehaviour
{
    [HideInInspector] public StatusEffectData StatusEffectData;

    public TextMeshProUGUI effectDescriptionText;
    public Image effectIconImage;

    public void SetStatusEffectData(StatusEffectData statusEffectData)
    {
        StatusEffectData = statusEffectData;
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (StatusEffectData != null)
        {
            effectDescriptionText.text = StatusEffectData.description;
            effectIconImage.sprite = StatusEffectData.effectSprite;
        }
        if (StatusEffectData.duration <=0)
        {
            gameObject.SetActive(false);
        }
        else
            gameObject.SetActive(true);
    }
}
