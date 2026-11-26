using UnityEngine;
using Enemy;
using UnityEngine.UIElements;
using System.Collections.Generic;
public class EnemiesPanel : BasePanel
{
    public BaseEnemy[] enemies;
    List<GameObject> enemyImages = new List<GameObject>();
    public GameObject enemyPanelPrefab;
    public Transform content;
    public EnemyDescriptionPanel descriptionPanel;


    void Start()
    {
        enemies = Resources.LoadAll<BaseEnemy>("EnemySO");
        foreach (BaseEnemy enemy in enemies)
        {
            GameObject instance = Instantiate(enemyPanelPrefab, transform);
            instance.GetComponent<EnemyPanel>().SetEnemy(Instantiate(enemy));
            instance.transform.SetParent(content);
            enemyImages.Add(instance);
            instance.GetComponent<EnemyPanel>().descriptionPanel = descriptionPanel;
        }
    }
    public override void OpenPanel()
    {
        base.OpenPanel();
        foreach (BaseEnemy enemy in enemies)
        {
            if (enemy.isUnlocked)
            {
                UnlockEnemy(enemy.enemyName);
            }
        }
    }
    public void UnlockEnemy(string enemyName)
    {
        foreach (BaseEnemy enemy in enemies)
        {
            if (enemy.enemyName == enemyName)
            {
                enemy.isUnlocked = true;
                foreach (GameObject image in enemyImages)
                {
                    if (image.name == enemyName)
                    {
                        image.GetComponent<Image>().tintColor = Color.white;
                    }
                }
            }
        }
    }
    public void ResetEnemies()
    {
        foreach (GameObject image in enemyImages)
        {
            image.GetComponent<Image>().tintColor = Color.black;
        }
    }
    public void CheckEnemies(List<string> names)
    {
        ResetEnemies();
        foreach(BaseEnemy enemy in enemies)
        {
            if (names.Contains(enemy.enemyName))
                UnlockEnemy(enemy.name);
            else
                enemy.isUnlocked = false;

        }
    }
}
