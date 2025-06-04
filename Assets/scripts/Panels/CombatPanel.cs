using Character;
using Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatPanel : BasePanel
{
    public GameObject combatImagePrefab;
    Image combatImage;
    public List<GameObject> combatIcons = new();
    
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
                Sprite sprite = character.GetComponent<SetEnemyUI>().enemy.enemySprite;
                combatImage.sprite = sprite;
            }
            combatImagePrefab.name = character.name;
            combatIcons.Add(character);
            Instantiate(combatImagePrefab, transform);
        }
    }
    public void UpdateCombatOrder()
    {
        Transform first = transform.GetChild(0);
        first.SetAsLastSibling();

    }
    public void RemoveFromCombat(GameObject character)
    {
        foreach (Transform child in transform)
        {
            if (child.name == character.name)
            {
                Destroy(child.gameObject);
                combatIcons.Remove(character);
            }
        }
    }
}
