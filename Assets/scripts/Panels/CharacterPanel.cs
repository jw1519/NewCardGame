using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Character;

public class CharacterPanel : MonoBehaviour
{
    public BaseCharacter character;

    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI characterDescriptionText;
    public Image characterImage;
    public void SetCharacter(BaseCharacter character)
    {
        this.character = character;
        characterNameText.text = character.characterName;
        characterDescriptionText.text = character.characterDescription;
        characterImage.sprite = character.characterImage;
    }
}
