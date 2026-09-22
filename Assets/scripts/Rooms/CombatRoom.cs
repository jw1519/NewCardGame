using Enemy;
using UnityEngine;

public class CombatRoom : BaseRoom
{
    CombatManager combatManager;
    EnemyFactory enemyFactory;
    public BaseEnemy enemyToSpawn;
    public int enemyAmount;

    public void RoomSetUp(BaseEnemy enemy, int amount)
    {
        combatManager = AssetManager.Instance.GetAsset("CombatManager").GetComponent<CombatManager>();
        enemyFactory = AssetManager.Instance.GetAsset("EnemyFactory").GetComponent<EnemyFactory>();
        enemyToSpawn = enemy;
        enemyAmount = amount;
    }
    public override void EnterRoom()
    {
        
        if (roomType == RoomType.Normal)
        {
            for (int i = 0; i < enemyAmount; i++)
            {
                GameObject instance = enemyFactory.CreateEnemy(enemyToSpawn);
                combatManager.AddToCombat(instance);
            }
        }
        else
        {
            GameObject instance = enemyFactory.CreateEnemy(enemyToSpawn);
            combatManager.AddToCombat(instance);
        }
        GameManager.instance.SetRoom(this);
        GameManager.instance.StartCombat();
        mapPanel.ClosePanel();
    }
}
