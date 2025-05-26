using Character;
using Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatPanel : BasePanel
{
    public GameObject combatImagePrefab;
    Image combatImage;
    
    // Start is called before the first frame update
    void Start()
    {
         combatImage = combatImagePrefab.GetComponentInChildren<Image>();
        foreach (GameObject character in CombatManager.instance.combatOrder)
        {
            if (character.GetComponent<SetCharacterUI>() != null)
            {
                Sprite sprite = CropTopHalf(character.GetComponent<SetCharacterUI>().character.characterSprite);
                combatImage.sprite = sprite;
            }
            else
            {
                Sprite sprite = CropTopHalf(character.GetComponent<BaseEnemy>().enemySprite);
                combatImage.sprite = sprite;
            }
            combatImagePrefab.name = character.name;
            Instantiate(combatImagePrefab, transform);
        }
    }
    public void UpdateCombatOrder()
    {

    }
    Sprite CropTopHalf(Sprite originalSprite)
    {
        Texture2D texture = originalSprite.texture;

        Rect originalRect = originalSprite.textureRect;
        float halfHeight = originalRect.height / 2f;

        Rect topHalfRect = new Rect(
            originalRect.x,
            originalRect.y + halfHeight,  // move up to top half
            originalRect.width,
            halfHeight
        );

        Vector2 pivot = new Vector2(0.5f, 0.5f); // center pivot
        float pixelsPerUnit = originalSprite.pixelsPerUnit;

        return Sprite.Create(texture, topHalfRect, pivot, pixelsPerUnit);
    }
}
