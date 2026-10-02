using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Enemy;

public class EnemyDescriptionPanel : BasePanel
{
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI nameText;
     public Image Image;

    public void SetEnemy(BaseEnemy enemy)
    {
        if (enemy.isUnlocked)
        {
            nameText.text = enemy.enemyName;
            descriptionText.text = enemy.description;
            Image.sprite = enemy.enemySprite;
        }
        else
        {
            nameText.text = "???";
            descriptionText.text = "This enemy is locked.";
            Image.sprite = enemy.enemySprite;
            Image.color = Color.gray;
        }
    }
}
