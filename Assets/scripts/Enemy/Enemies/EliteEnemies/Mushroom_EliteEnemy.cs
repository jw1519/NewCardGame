using Enemy;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mushroom_EliteEnemy", menuName = "Enemy/Elite/Mushroom")]
public class Mushroom_EliteEnemy : BaseEnemy, ISummon
{
    public BaseEnemy enemyToSpawn;
    public int maxEnemySummons;
    public List<BaseEnemy> enemyList; //list of alive enemy summons
    public AbilityTargetType ability2TargetType;

    private void OnEnable()
    {
        enemydied += OnEnemyDied;
    }
    private void OnDestroy()
    {
        enemydied -= OnEnemyDied;
    }
    public void SummonEnemy(BaseEnemy enemyToSummon)
    {
        Debug.Log("Summoning enemy: " + enemyToSummon.name);
        enemyList.Add(enemyToSummon);
    }
    public void OnEnemyDied()
    {
        // Remove the dead enemy from the list
        enemyList.RemoveAll(enemy => !enemy.isAlive);
    }

    public override void UseAbility(GameObject target)
    {
        if (ability2TargetType == AbilityTargetType.Self)
        {
            if (enemyList.Count >= maxEnemySummons)
            {
                Debug.Log("Max summons reached");
                return;
            }
            //EventQueue.EnqueueEvent(new EnemySummonEvent(enemyToSpawn, this));
        }
        else if (ability2TargetType == AbilityTargetType.Player)
        {
            // Implement ability logic for targeting the player
        }
    }

}
