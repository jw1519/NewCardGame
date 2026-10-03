using UnityEngine;
using Enemy;
using UnityEngine.UI;

public class EnemyPanel : MonoBehaviour
{
    BaseEnemy enemy;
    public Image enemyImage;
    public EnemyDescriptionPanel descriptionPanel;

    public void SetEnemy(BaseEnemy enemy)
    {
        this.enemy = Instantiate(enemy);
        name = enemy.enemyName;
        enemyImage.sprite = enemy.enemySprite;
        enemyImage.color = enemy.isUnlocked ? Color.white : Color.black;
    }
    public void Unlock()
    {
        enemy.isUnlocked = true;
        enemyImage.color = Color.white;
    }
    public void OnClick()
    {
        descriptionPanel.SetEnemy(enemy);
    }
}
