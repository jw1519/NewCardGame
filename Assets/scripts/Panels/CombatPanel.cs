using Character;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

public class CombatPanel : BasePanel
{
    public GameObject combatImagePrefab;
    Image combatImage;
    
    void Start()
    {
         combatImage = combatImagePrefab.GetComponentInChildren<Image>();
        foreach (GameObject character in CombatManager.instance.combatOrder)
        {
            if (character.GetComponent<SetCharacterUI>() != null)
            {
                Sprite sprite = character.GetComponent<SetCharacterUI>().character.characterSprite;
                combatImage.sprite = sprite;
            }
            else
            {
                Sprite sprite = character.GetComponent<BaseEnemy>().enemySprite;
                combatImage.sprite = sprite;
            }
            combatImagePrefab.name = character.name;
            Instantiate(combatImagePrefab, transform);
        }
    }
    public void UpdateCombatOrder()
    {

    }
}
