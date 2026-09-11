using Character;
using UnityEngine;
using System.Collections.Generic;
using Card;

public class CharacterSelectPanel : BasePanel
{
    public List<BaseCharacter> characters;
    public List<GameObject> characterpanels;

    int selectedCharacterIndex = 0;

    public GameObject character;
    public Transform charaterParent;

    public GameObject characterPanelPrefab;

    private void Awake()
    {
        Create();
        characterpanels[selectedCharacterIndex].SetActive(true);
    }

    public void Create()
    {
        foreach (BaseCharacter character in characters)
        {
            GameObject panel = Instantiate(characterPanelPrefab, transform);
            panel.GetComponent<CharacterPanel>().SetCharacter(character);
            characterpanels.Add(panel);
            panel.SetActive(false);
        }
    }

    public void SelectCharacter()
    {
        if (character != null)
        {
            character.GetComponent<SetCharacterUI>().character = characters[selectedCharacterIndex];
            AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("PlayerStatsPanel").GetComponent<PlayerStatsPanel>().SetUp(characters[selectedCharacterIndex]);
            CardPool.instance.SetUp(characters[selectedCharacterIndex].startingDeck);
            character.GetComponent<SetCharacterUI>().SetUp();
            ClosePanel();
        }
    }
    public void NextCharacter()
    {
        characterpanels[selectedCharacterIndex].SetActive(false);

        selectedCharacterIndex++;
        if (selectedCharacterIndex >= characters.Count)
            selectedCharacterIndex = 0;
        characterpanels[selectedCharacterIndex].SetActive(true);
    }
    public void PreviousCharacter()
    {
        characterpanels[selectedCharacterIndex].SetActive(false);
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
            selectedCharacterIndex = characters.Count - 1;
        characterpanels[selectedCharacterIndex].SetActive(true);
    }
}
