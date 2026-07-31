using Enemy;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_EliteEnemy : BaseEnemy
{
    public BaseEnemy enemyToSpawn;
    public List<BaseEnemy> enemyList; //list of alive enemy summons
    public AbilityTargetType ability2TargetType;
    public override void UseAbility(GameObject target)
    {
        base.UseAbility(target);
    }
}
