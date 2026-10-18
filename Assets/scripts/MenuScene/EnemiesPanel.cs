using UnityEngine;
using Enemy;
using UnityEngine.UIElements;
using System.Collections.Generic;
public class EnemiesPanel : BasePanel, ISave
{
    public BaseEnemy[] enemies;
    List<GameObject> enemyImages = new List<GameObject>();
    public GameObject enemyPanelPrefab;
    public Transform content;
    public EnemyDescriptionPanel descriptionPanel;


    void Start()
    {
        foreach (BaseEnemy enemy in enemies)
        {
            GameObject instance = Instantiate(enemyPanelPrefab, transform);
            instance.GetComponent<EnemyPanel>().SetEnemy(enemy);
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

    public void SaveData(ref GameData data)
    {

    }

    public void LoadData(GameData data)
    {

    }

}
